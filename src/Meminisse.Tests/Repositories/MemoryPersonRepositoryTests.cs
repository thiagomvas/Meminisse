using Meminisse.Application.Repositories;
using Meminisse.Core.Entities;
using Meminisse.Core.ValueTypes;
using Microsoft.EntityFrameworkCore;

namespace Meminisse.Tests.Repositories;
[TestFixture]
internal class MemoryPersonRepositoryTests : BaseRepositoryTest
{
    private MemoryPersonRepository _repository;
    private MemoryRepository _memoryRepository;
    private PersonRepository _personRepository;

    public override void SetUp()
    {
        base.SetUp();
        _repository = new MemoryPersonRepository(Context);
        _memoryRepository = new MemoryRepository(Context);
        _personRepository = new PersonRepository(Context);
    }

    [Test]
    public async Task AddAsync_ShouldAddMemoryPerson()
    {
        var memory = new Memory { Title = "Test Memory", Description = "Test Description", Date = DateTime.Now, Emotion = Emotion.Joy };
        var person = new Person { FullName = "John Doe" };
        await _memoryRepository.AddAsync(memory);
        await _personRepository.AddAsync(person);

        var memoryPerson = new MemoryPerson { MemoryId = memory.Id, PersonId = person.Id };
        await _repository.AddAsync(memoryPerson);
        var result = await _repository.GetAllAsync();

        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].MemoryId, Is.EqualTo(memory.Id));
        Assert.That(result[0].PersonId, Is.EqualTo(person.Id));
    }

    [Test]
    public async Task AddRangeAsync_ShouldAddMemoryPeople()
    {
        var memory = new Memory { Title = "Test Memory", Description = "Test Description", Date = DateTime.Now, Emotion = Emotion.Joy };
        var person1 = new Person { FullName = "John Doe" };
        var person2 = new Person { FullName = "Jane Doe" };
        await _memoryRepository.AddAsync(memory);
        await _personRepository.AddAsync(person1);
        await _personRepository.AddAsync(person2);

        var memoryPeople = new List<MemoryPerson>
        {
            new MemoryPerson { MemoryId = memory.Id, PersonId = person1.Id },
            new MemoryPerson { MemoryId = memory.Id, PersonId = person2.Id }
        };
        await _repository.AddRangeAsync(memoryPeople);
        var result = await _repository.GetAllAsync();

        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task DeleteAsync_ShouldDeleteMemoryPerson()
    {
        var memory = new Memory { Title = "Test Memory", Description = "Test Description", Date = DateTime.Now, Emotion = Emotion.Joy };
        var person = new Person { FullName = "John Doe" };
        await _memoryRepository.AddAsync(memory);
        await _personRepository.AddAsync(person);

        var memoryPerson = new MemoryPerson { MemoryId = memory.Id, PersonId = person.Id };
        await _repository.AddAsync(memoryPerson);
        await _repository.DeleteAsync(memoryPerson);
        var result = await _repository.GetAllAsync();

        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task PersonRepositoryDeleteAsync_ShouldDeleteMemoryPerson()
    {
        var memory = new Memory { Title = "Test Memory", Description = "Test Description", Date = DateTime.Now, Emotion = Emotion.Joy };
        var person = new Person { FullName = "John Doe" };

        await _memoryRepository.AddAsync(memory);
        await _personRepository.AddAsync(person);

        var memoryPerson = new MemoryPerson { MemoryId = memory.Id, PersonId = person.Id };
        await _repository.AddAsync(memoryPerson);
        await _personRepository.DeleteAsync(person);

        var result = await _repository.GetAllAsync();
        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task MemoryRepositoryDeleteAsync_ShouldDeleteMemoryPerson()
    {
        var memory = new Memory { Title = "Test Memory", Description = "Test Description", Date = DateTime.Now, Emotion = Emotion.Joy };
        var person = new Person { FullName = "John Doe" };

        await _memoryRepository.AddAsync(memory);
        await _personRepository.AddAsync(person);

        var memoryPerson = new MemoryPerson { MemoryId = memory.Id, PersonId = person.Id };
        await _repository.AddAsync(memoryPerson);

        // Reload to ensure EF is tracking entities
        memory = await _memoryRepository.GetAsync(memory.Id);

        await _memoryRepository.DeleteAsync(memory);

        var result = await _repository.Get()
            .Where(mp => mp.MemoryId == memory.Id)
            .ToListAsync();

        Assert.That(result, Is.Empty);
    }



    [Test]
    public async Task DeleteAsync_ById_ShouldThrown()
    {
        var memory = new Memory { Title = "Test Memory", Description = "Test Description", Date = DateTime.Now, Emotion = Emotion.Joy };
        var person = new Person { FullName = "John Doe" };
        await _memoryRepository.AddAsync(memory);
        await _personRepository.AddAsync(person);

        var memoryPerson = new MemoryPerson { MemoryId = memory.Id, PersonId = person.Id };
        await _repository.AddAsync(memoryPerson);
        Assert.ThrowsAsync<ArgumentException>(() => _repository.DeleteAsync(memoryPerson.MemoryId));
        Assert.ThrowsAsync<ArgumentException>(() => _repository.DeleteAsync(memoryPerson.PersonId));

    }

    [Test]
    public async Task GetAllAsync_ShouldReturnAllMemoryPeople()
    {
        var memory = new Memory { Title = "Test Memory", Description = "Test Description", Date = DateTime.Now, Emotion = Emotion.Joy };
        var person1 = new Person { FullName = "John Doe" };
        var person2 = new Person { FullName = "Jane Doe" };
        await _memoryRepository.AddAsync(memory);
        await _personRepository.AddAsync(person1);
        await _personRepository.AddAsync(person2);

        var memoryPeople = new List<MemoryPerson>
        {
            new MemoryPerson { MemoryId = memory.Id, PersonId = person1.Id },
            new MemoryPerson { MemoryId = memory.Id, PersonId = person2.Id }
        };
        await _repository.AddRangeAsync(memoryPeople);
        var result = await _repository.GetAllAsync();

        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task UpdateAsync_WhenUpdatingExistingPersonOrMemoryId_ShouldThrow()
    {
        var memory = new Memory { Title = "Test Memory", Description = "Test Description", Date = DateTime.Now, Emotion = Emotion.Joy };
        var person = new Person { FullName = "John Doe" };
        await _memoryRepository.AddAsync(memory);
        await _personRepository.AddAsync(person);
        var memoryPerson = new MemoryPerson { MemoryId = memory.Id, PersonId = person.Id };
        await _repository.AddAsync(memoryPerson);
        memoryPerson.PersonId = 999;
        Assert.ThrowsAsync<InvalidOperationException>(() => _repository.UpdateAsync(memoryPerson));
    }
}