using Meminisse.Application.Repositories;
using Meminisse.Core.Entities;
using Meminisse.Core.ValueTypes;

namespace Meminisse.Tests.Repositories;
internal class MemoryTagRepositoryTests : BaseRepositoryTest
{
    private MemoryTagRepository _repository;
    private MemoryRepository _memoryRepository;
    private TagRepository _tagRepository;
    public override void SetUp()
    {
        base.SetUp();
        _repository = new MemoryTagRepository(Context);
        _memoryRepository = new MemoryRepository(Context);
        _tagRepository = new TagRepository(Context);
    }
    [Test]
    public async Task AddAsync_ShouldAddMemoryTag()
    {
        var memory = new Memory
        {
            Title = "Test Memory",
            Description = "Test Description",
            Date = DateTime.Now,
            Emotion = Emotion.Joy
        };
        await _memoryRepository.AddAsync(memory);
        var tag = new Tag
        {
            Name = "Test Tag"
        };
        await _tagRepository.AddAsync(tag);
        var memoryTag = new MemoryTag
        {
            MemoryId = memory.Id,
            TagId = tag.Id
        };
        await _repository.AddAsync(memoryTag);
        var result = await _repository.GetAllAsync();
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].MemoryId, Is.EqualTo(memory.Id));
        Assert.That(result[0].TagId, Is.EqualTo(tag.Id));
    }
    [Test]
    public async Task AddRangeAsync_ShouldAddMemoryTags()
    {
        var memory = new Memory
        {
            Title = "Test Memory",
            Description = "Test Description",
            Date = DateTime.Now,
            Emotion = Emotion.Joy
        };
        await _memoryRepository.AddAsync(memory);
        var tag1 = new Tag
        {
            Name = "Tag 1"
        };
        await _tagRepository.AddAsync(tag1);
        var tag2 = new Tag
        {
            Name = "Tag 2"
        };
        await _tagRepository.AddAsync(tag2);
        var memoryTags = new List<MemoryTag>
        {
            new MemoryTag { MemoryId = memory.Id, TagId = tag1.Id },
            new MemoryTag { MemoryId = memory.Id, TagId = tag2.Id }
        };
        await _repository.AddRangeAsync(memoryTags);
        var result = await _repository.GetAllAsync();
        Assert.That(result, Has.Count.EqualTo(2));
    }
    [Test]
    public async Task DeleteAsync_ShouldDeleteMemoryTag()
    {
        var memory = new Memory
        {
            Title = "Test Memory",
            Description = "Test Description",
            Date = DateTime.Now,
            Emotion = Emotion.Joy
        };
        await _memoryRepository.AddAsync(memory);
        var tag = new Tag
        {
            Name = "Test Tag"
        };
        await _tagRepository.AddAsync(tag);
        var memoryTag = new MemoryTag
        {
            MemoryId = memory.Id,
            TagId = tag.Id
        };
        await _repository.AddAsync(memoryTag);
        var result = await _repository.GetAllAsync();
        Assert.That(result, Has.Count.EqualTo(1));
        await _repository.DeleteAsync(memoryTag);
        result = await _repository.GetAllAsync();
        Assert.That(result, Has.Count.EqualTo(0));
    }

    [Test]
    public async Task MemoryDeleteAsync_ShouldDeleteExistingMemoryTags()
    {
        var memory = new Memory
        {
            Title = "Test Memory",
            Description = "Test Description",
            Date = DateTime.Now,
            Emotion = Emotion.Joy
        };
        await _memoryRepository.AddAsync(memory);
        var tag = new Tag
        {
            Name = "Test Tag"
        };
        await _tagRepository.AddAsync(tag);
        var memoryTag = new MemoryTag
        {
            MemoryId = memory.Id,
            TagId = tag.Id

        };

        await _repository.AddAsync(memoryTag);
        var result = await _repository.GetAllAsync();
        Assert.That(result, Has.Count.EqualTo(1));
        await _memoryRepository.DeleteAsync(memory);
        result = await _repository.GetAllAsync();

        Assert.That(result, Has.Count.EqualTo(0));
    }

    [Test]
    public async Task TagDeleteAsync_ShouldDeleteExistingMemoryTags()
    {
        var memory = new Memory
        {
            Title = "Test Memory",
            Description = "Test Description",
            Date = DateTime.Now,
            Emotion = Emotion.Joy
        };
        await _memoryRepository.AddAsync(memory);
        var tag = new Tag
        {
            Name = "Test Tag"
        };
        await _tagRepository.AddAsync(tag);
        var memoryTag = new MemoryTag
        {
            MemoryId = memory.Id,
            TagId = tag.Id
        };

        await _repository.AddAsync(memoryTag);
        var result = await _repository.GetAllAsync();

        Assert.That(result, Has.Count.EqualTo(1));

        await _tagRepository.DeleteAsync(tag);
        result = await _repository.GetAllAsync();

        Assert.That(result, Has.Count.EqualTo(0));
    }
}