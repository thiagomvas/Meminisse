using Meminisse.Application.Repositories;
using Meminisse.Core.Entities;

namespace Meminisse.Tests.Repositories;
internal class TagRepositoryTests : BaseRepositoryTest
{
    private TagRepository _repository;
    public override void SetUp()
    {
        base.SetUp();
        _repository = new TagRepository(Context);
    }
    [Test]
    public async Task AddAsync_ShouldAddTag()
    {
        var tag = new Tag
        {
            Name = "Test Tag"
        };
        await _repository.AddAsync(tag);
        var result = await _repository.GetAllAsync();
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].Name, Is.EqualTo("Test Tag"));
    }
    [Test]
    public async Task AddRangeAsync_ShouldAddTags()
    {
        var tags = new List<Tag>
        {
            new Tag { Name = "Tag 1" },
            new Tag { Name = "Tag 2" }
        };
        await _repository.AddRangeAsync(tags);
        var result = await _repository.GetAllAsync();
        Assert.That(result, Has.Count.EqualTo(2));
    }
    [Test]
    public async Task DeleteAsync_ShouldDeleteTag()
    {
        var tag = new Tag
        {
            Name = "Test Tag"
        };
        await _repository.AddAsync(tag);
        var result = await _repository.GetAllAsync();
        Assert.That(result, Has.Count.EqualTo(1));
        await _repository.DeleteAsync(tag);
        result = await _repository.GetAllAsync();
        Assert.That(result, Has.Count.EqualTo(0));
    }
    [Test]
    public async Task UpdateAsync_ShouldUpdateTag()
    {
        var tag = new Tag
        {
            Name = "Test Tag"
        };
        await _repository.AddAsync(tag);
        var result = await _repository.GetAllAsync();
        Assert.That(result, Has.Count.EqualTo(1));
        tag.Name = "Updated Tag";
        await _repository.UpdateAsync(tag);
        result = await _repository.GetAllAsync();
        Assert.That(result[0].Name, Is.EqualTo("Updated Tag"));
    }

    [Test]
    public async Task DeleteAsync_ById_ShouldDeleteTag()
    {
        var tag = new Tag
        {
            Name = "Test Tag"
        };
        await _repository.AddAsync(tag);
        var result = await _repository.GetAllAsync();
        Assert.That(result, Has.Count.EqualTo(1));
        await _repository.DeleteAsync(tag.Id);
        result = await _repository.GetAllAsync();
        Assert.That(result, Has.Count.EqualTo(0));
    }

    [Test]
    public async Task GetAllAsync_ShouldReturnAllTags()
    {
        var tags = new List<Tag>
        {
            new Tag { Name = "Tag 1" },
            new Tag { Name = "Tag 2" }
        };
        await _repository.AddRangeAsync(tags);
        var result = await _repository.GetAllAsync();
        Assert.That(result, Has.Count.EqualTo(2));
    }
}