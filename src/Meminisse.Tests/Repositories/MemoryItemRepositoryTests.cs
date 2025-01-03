using Meminisse.Application.Repositories;
using Meminisse.Core.Entities;
using Meminisse.Core.ValueTypes;

namespace Meminisse.Tests.Repositories;
[TestFixture]
internal class MemoryItemRepositoryTests : BaseRepositoryTest
{
    private MemoryItemRepository _repository;

    public override void SetUp()
    {
        base.SetUp();
        _repository = new MemoryItemRepository(Context);
    }

    [Test]
    public async Task AddAsync_ShouldAddMemoryItem()
    {
        var memoryItem = new MemoryItem
        {
            Content = "Test Content",
            DateAdded = DateTime.Now,
            Type = MemoryType.Text
        };

        await _repository.AddAsync(memoryItem);
        var result = await _repository.GetAllAsync();

        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].Content, Is.EqualTo("Test Content"));
    }

    [Test]
    public async Task AddRangeAsync_ShouldAddMemoryItems()
    {
        var memoryItems = new List<MemoryItem>
        {
            new MemoryItem { Content = "Content 1", DateAdded = DateTime.Now, Type = MemoryType.Text },
            new MemoryItem { Content = "Content 2", DateAdded = DateTime.Now, Type = MemoryType.Image }
        };

        await _repository.AddRangeAsync(memoryItems);
        var result = await _repository.GetAllAsync();

        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task DeleteAsync_ShouldDeleteMemoryItem()
    {
        var memoryItem = new MemoryItem
        {
            Content = "Test Content",
            DateAdded = DateTime.Now,
            Type = MemoryType.Text
        };

        await _repository.AddAsync(memoryItem);
        await _repository.DeleteAsync(memoryItem);
        var result = await _repository.GetAllAsync();

        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task DeleteAsync_ById_ShouldDeleteMemoryItem()
    {
        var memoryItem = new MemoryItem
        {
            Content = "Test Content",
            DateAdded = DateTime.Now,
            Type = MemoryType.Text
        };

        await _repository.AddAsync(memoryItem);
        await _repository.DeleteAsync(memoryItem.Id);
        var result = await _repository.GetAllAsync();

        Assert.That(result.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task GetAllAsync_ShouldReturnAllMemoryItems()
    {
        var memoryItems = new List<MemoryItem>
        {
            new MemoryItem {  Content = "Content 1", DateAdded = DateTime.Now, Type = MemoryType.Text },
            new MemoryItem {  Content = "Content 2", DateAdded = DateTime.Now, Type = MemoryType.Image }
        };

        await _repository.AddRangeAsync(memoryItems);
        var result = await _repository.GetAllAsync();

        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task UpdateAsync_ShouldUpdateMemoryItem()
    {
        var memoryItem = new MemoryItem
        {
            
            Content = "Test Content",
            DateAdded = DateTime.Now,
            Type = MemoryType.Text
        };

        await _repository.AddAsync(memoryItem);
        memoryItem.Content = "Updated Content";
        await _repository.UpdateAsync(memoryItem);
        var result = await _repository.GetAllAsync();

        Assert.That(result[0].Content, Is.EqualTo("Updated Content"));
    }
}