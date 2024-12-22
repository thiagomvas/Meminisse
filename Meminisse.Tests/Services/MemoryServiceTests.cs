using Meminisse.Application.Repositories;
using Meminisse.Application.Services;
using Meminisse.Core.Abstractions;
using Meminisse.Core.Entities;
using Meminisse.Core.ValueTypes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Meminisse.Tests.Services;
internal class MemoryServiceTests : BaseServiceTest
{
    private MemoryService _memoryService;
    private IRepository<MemoryTag> _memoryTagRepository;
    private IRepository<MemoryPerson> _memoryPersonRepository;
    private IRepository<Tag> _tagRepository;
    private IRepository<Person> _personRepository;
    private IRepository<Memory> _memoryRepository;

    public override void SetUp()
    {
        Services.AddScoped<MemoryService>();
        Services.AddScoped<IRepository<Person>, PersonRepository>();
        Services.AddScoped<IRepository<Tag>, TagRepository>();
        Services.AddScoped<IRepository<Memory>, MemoryRepository>();
        Services.AddScoped<IRepository<MemoryPerson>, MemoryPersonRepository>();
        Services.AddScoped<IRepository<MemoryTag>, MemoryTagRepository>();

        base.SetUp();
        _memoryService = Provider.GetRequiredService<MemoryService>();
        _memoryTagRepository = Provider.GetRequiredService<IRepository<MemoryTag>>();
        _memoryPersonRepository = Provider.GetRequiredService<IRepository<MemoryPerson>>();
        _tagRepository = Provider.GetRequiredService<IRepository<Tag>>();
        _personRepository = Provider.GetRequiredService<IRepository<Person>>();
        _memoryRepository = Provider.GetRequiredService<IRepository<Memory>>();
    }

    [Test]
    public async Task AddPersonAsync_ShouldAddPersonToMemory()
    {
        var memory = new Memory { Id = Guid.NewGuid(), Title = "Test Memory" };
        var person = new Person { Id = Guid.NewGuid(), FullName = "Test Person" };

        await _memoryRepository.AddAsync(memory);
        await _personRepository.AddAsync(person);

        await _memoryService.AddPersonAsync(memory.Id, person.Id);

        var memoryPerson = await _memoryPersonRepository.Get().FirstOrDefaultAsync(mp => mp.MemoryId == memory.Id && mp.PersonId == person.Id);
        Assert.That(memoryPerson, Is.Not.Null);
    }

    [Test]
    public async Task AddTagAsync_ShouldAddTagToMemory()
    {
        var memory = new Memory { Id = Guid.NewGuid(), Title = "Test Memory" };
        var tag = new Tag { Id = Guid.NewGuid(), Name = "Test Tag" };

        await _memoryRepository.AddAsync(memory);
        await _tagRepository.AddAsync(tag);

        await _memoryService.AddTagAsync(memory.Id, tag.Id);
        var memoryTag = await _memoryTagRepository.Get().FirstOrDefaultAsync(mt => mt.MemoryId == memory.Id && mt.TagId == tag.Id);
        Assert.That(memoryTag, Is.Not.Null);
    }

    [Test]
    public async Task GetByPersonAsync_ShouldReturnMemoriesForPerson()
    {
        var person = new Person { Id = Guid.NewGuid(), FullName = "Test Person" };
        var memory = new Memory { Id = Guid.NewGuid(), Title = "Test Memory" };
        var memoryPerson = new MemoryPerson { MemoryId = memory.Id, PersonId = person.Id };

        await _personRepository.AddAsync(person);
        await _memoryRepository.AddAsync(memory);
        await _memoryPersonRepository.AddAsync(memoryPerson);

        var result = await _memoryService.GetByPersonAsync(person.Id);

        Assert.That(result, Has.Exactly(1).Items);
        Assert.That(result.First().Id, Is.EqualTo(memory.Id));
    }

    [Test]
    public async Task GetInCommonAsync_ShouldReturnCommonMemories()
    {
        var person1 = new Person { Id = Guid.NewGuid(), FullName = "Person 1" };
        var person2 = new Person { Id = Guid.NewGuid(), FullName = "Person 2" };
        var memory = new Memory { Id = Guid.NewGuid(), Title = "Common Memory" };
        var outsideMemory = new Memory { Id = Guid.NewGuid(), Title = "Outside Memory" };

        await _personRepository.AddAsync(person1);
        await _personRepository.AddAsync(person2);
        await _memoryRepository.AddAsync(memory);
        await _memoryRepository.AddAsync(outsideMemory);
        await _memoryService.AddPersonAsync(memory.Id, person1.Id);
        await _memoryService.AddPersonAsync(memory.Id, person2.Id);

        var result = await _memoryService.GetInCommonAsync(new List<Guid> { person1.Id, person2.Id });

        Assert.That(result, Has.Exactly(1).Items);
        Assert.That(result.First().Id, Is.EqualTo(memory.Id));
    }

    [Test]
    public async Task GetByEmotionAsync_ShouldReturnMemoriesWithEmotion()
    {
        var memory = new Memory { Id = Guid.NewGuid(), Title = "Test Memory", Emotion = Emotion.Joy };
        var sadMemory = new Memory { Id = Guid.NewGuid(), Title = "Sad Memory", Emotion = Emotion.Sadness };
        await _memoryRepository.AddAsync(memory);
        await _memoryRepository.AddAsync(sadMemory);

        var result = await _memoryService.GetByEmotionAsync(Emotion.Joy);

        Assert.That(result, Has.Exactly(1).Items);
        Assert.That(result.First().Id, Is.EqualTo(memory.Id));
    }

    [Test]
    public async Task RemovePersonAsync_ShouldRemovePersonFromMemory()
    {
        var person = new Person { Id = Guid.NewGuid(), FullName = "Test Person" };
        var memory = new Memory { Id = Guid.NewGuid(), Title = "Test Memory" };
        var memoryPerson = new MemoryPerson { MemoryId = memory.Id, PersonId = person.Id };

        await _personRepository.AddAsync(person);
        await _memoryRepository.AddAsync(memory);
        await _memoryPersonRepository.AddAsync(memoryPerson);

        await _memoryService.RemovePersonAsync(memory.Id, person.Id);

        var result = await Context.MemoryPeople.FirstOrDefaultAsync(mp => mp.MemoryId == memory.Id && mp.PersonId == person.Id);
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task RemoveTagAsync_ShouldRemoveTagFromMemory()
    {
        var tag = new Tag { Id = Guid.NewGuid(), Name = "Test Tag" };
        var memory = new Memory { Id = Guid.NewGuid(), Title = "Test Memory" };

        await _tagRepository.AddAsync(tag);
        await _memoryRepository.AddAsync(memory);
        await _memoryService.AddTagAsync(memory.Id, tag.Id);

        await _memoryService.RemoveTagAsync(memory.Id, tag.Id);

        var result = await Context.MemoryTags.FirstOrDefaultAsync(mt => mt.MemoryId == memory.Id && mt.TagId == tag.Id);
        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task GetByIdAsync_ShouldReturnMemoryById()
    {
        var memory = new Memory { Id = Guid.NewGuid(), Title = "Test Memory" };

        await _memoryRepository.AddAsync(memory);

        var result = await _memoryService.GetByIdAsync(memory.Id);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(memory.Id));
    }
}
