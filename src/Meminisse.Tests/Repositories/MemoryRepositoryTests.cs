using Meminisse.Application.Repositories;
using Meminisse.Core.Entities;
using Meminisse.Core.ValueTypes;

namespace Meminisse.Tests.Repositories;

internal class MemoryRepositoryTests : BaseRepositoryTest
{
    private MemoryRepository _repository;
    private MemoryItemRepository _memoryItemRepository;

    public override void SetUp()
    {
        base.SetUp();
        _repository = new MemoryRepository(Context);
        _memoryItemRepository = new MemoryItemRepository(Context);
    }

    [Test]
    public async Task AddAsync_ShouldAddMemory()
    {
        var memory = new Memory
        {
            Title = "Test Memory",
            Description = "Test Description",
            Date = DateTime.Now,
            Emotion = Emotion.Joy
        };

        await _repository.AddAsync(memory);
        var result = await _repository.GetAllAsync();

        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].Title, Is.EqualTo("Test Memory"));
    }

    [Test]
    public async Task AddRangeAsync_ShouldAddMemories()
    {
        var memories = new List<Memory>
            {
                new Memory { Title = "Memory 1", Description = "Description 1", Date = DateTime.Now, Emotion = Emotion.Joy },
                new Memory { Title = "Memory 2", Description = "Description 2", Date = DateTime.Now, Emotion = Emotion.Love }
            };

        await _repository.AddRangeAsync(memories);
        var result = await _repository.GetAllAsync();

        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task DeleteAsync_ShouldDeleteMemory()
    {
        var memory = new Memory
        {
            Title = "Test Memory",
            Description = "Test Description",
            Date = DateTime.Now,
            Emotion = Emotion.Joy
        };

        await _repository.AddAsync(memory);
        await _repository.DeleteAsync(memory);
        var result = await _repository.GetAllAsync();

        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task DeleteAsync_ShouldDeleteAttachedMemoryItems()
    {
        var memory = new Memory
        {
            Title = "Test Memory",
            Description = "Test Description",
            Date = DateTime.Now,
            Emotion = Emotion.Joy
        };

        await _repository.AddAsync(memory);

        var memoryItem = new MemoryItem
        {
            MemoryId = memory.Id,
            Content = "Test Content",
            DateAdded = DateTime.Now,
            Type = MemoryType.Text
        };

        var memoryItem2 = new MemoryItem
        {
            MemoryId = memory.Id,
            Content = "Test Content 2",
            DateAdded = DateTime.Now,
            Type = MemoryType.Text
        };

        await _memoryItemRepository.AddAsync(memoryItem);
        await _memoryItemRepository.AddAsync(memoryItem2);

        await _repository.DeleteAsync(memory);

        var result = await _memoryItemRepository.GetAllAsync();
        Assert.That(result, Is.Empty);
    }


    [Test]
    public async Task DeleteAsync_ById_ShouldDeleteMemory()
    {
        var memory = new Memory
        {
            Title = "Test Memory",
            Description = "Test Description",
            Date = DateTime.Now,
            Emotion = Emotion.Joy
        };

        await _repository.AddAsync(memory);
        await _repository.DeleteAsync(memory.Id);
        var result = await _repository.GetAllAsync();

        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task GetAllAsync_ShouldReturnAllMemories()
    {
        var memories = new List<Memory>
            {
                new Memory { Title = "Memory 1", Description = "Description 1", Date = DateTime.Now, Emotion = Emotion.Joy },
                new Memory { Title = "Memory 2", Description = "Description 2", Date = DateTime.Now, Emotion = Emotion.Love }
            };

        await _repository.AddRangeAsync(memories);
        var result = await _repository.GetAllAsync();

        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task UpdateAsync_ShouldUpdateMemory()
    {
        var memory = new Memory
        {
            Title = "Test Memory",
            Description = "Test Description",
            Date = DateTime.Now,
            Emotion = Emotion.Joy
        };

        await _repository.AddAsync(memory);
        memory.Title = "Updated Memory";
        await _repository.UpdateAsync(memory);
        var result = await _repository.GetAllAsync();

        Assert.That(result[0].Title, Is.EqualTo("Updated Memory"));
    }
}