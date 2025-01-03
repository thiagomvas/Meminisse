using Meminisse.Application.Repositories;
using Meminisse.Core.Entities;

namespace Meminisse.Tests.Repositories;
internal class PersonTagRepositoryTests : BaseRepositoryTest
{
    private PersonTagRepository _repository;
    private PersonRepository _personRepository;
    private TagRepository _tagRepository;
    public override void SetUp()
    {
        base.SetUp();
        _repository = new PersonTagRepository(Context);
        _personRepository = new PersonRepository(Context);
        _tagRepository = new TagRepository(Context);
    }
    [Test]
    public async Task AddAsync_ShouldAddPersonTag()
    {
        var person = new Person
        {
            FullName = "Test Person"
        };
        await _personRepository.AddAsync(person);
        var tag = new Tag
        {
            Name = "Test Tag"
        };
        await _tagRepository.AddAsync(tag);
        var personTag = new PersonTag
        {
            PersonId = person.Id,
            TagId = tag.Id
        };
        await _repository.AddAsync(personTag);
        var result = await _repository.GetAllAsync();
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].PersonId, Is.EqualTo(person.Id));
        Assert.That(result[0].TagId, Is.EqualTo(tag.Id));
    }
    [Test]
    public async Task AddRangeAsync_ShouldAddPersonTags()
    {
        var person = new Person
        {
            FullName = "Test Person"
        };
        await _personRepository.AddAsync(person);
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
        var personTags = new List<PersonTag>
        {
            new PersonTag { PersonId = person.Id, TagId = tag1.Id },
            new PersonTag { PersonId = person.Id, TagId = tag2.Id }
        };
        await _repository.AddRangeAsync(personTags);
        var result = await _repository.GetAllAsync();
        Assert.That(result, Has.Count.EqualTo(2));
    }
    [Test]
    public async Task DeleteAsync_ShouldDeletePersonTag()
    {
        var person = new Person
        {
            FullName = "Test Person"
        };
        await _personRepository.AddAsync(person);
        var tag = new Tag
        {
            Name = "Test Tag"
        };
        await _tagRepository.AddAsync(tag);
        var personTag = new PersonTag
        {
            PersonId = person.Id,
            TagId = tag.Id
        };
        await _repository.AddAsync(personTag);
        await _repository.DeleteAsync(personTag);

        var result = await _repository.GetAllAsync();
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task GetAllAsync_ShouldReturnAllPersonTags()
    {
        var person = new Person
        {
            FullName = "Test Person"
        };
        await _personRepository.AddAsync(person);
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
        var personTags = new List<PersonTag>
        {
            new PersonTag { PersonId = person.Id, TagId = tag1.Id },
            new PersonTag { PersonId = person.Id, TagId = tag2.Id }
        };
        await _repository.AddRangeAsync(personTags);
        var result = await _repository.GetAllAsync();
        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task PersonDeleteAsync_ShouldDeleteExistingPersonTags()
    {
        var person = new Person
        {
            FullName = "Test Person"
        };
        await _personRepository.AddAsync(person);
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
        var personTags = new List<PersonTag>
        {
            new PersonTag { PersonId = person.Id, TagId = tag1.Id },
            new PersonTag { PersonId = person.Id, TagId = tag2.Id }
        };
        await _repository.AddRangeAsync(personTags);
        await _personRepository.DeleteAsync(person);
        var result = await _repository.GetAllAsync();
        Assert.That(result, Is.Empty);
    }

    [Test]
    public async Task TagDeleteAsync_ShouldDeleteExistingPersonTags()
    {
        var person = new Person
        {
            FullName = "Test Person"
        };
        await _personRepository.AddAsync(person);
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
        var personTags = new List<PersonTag>
        {
            new PersonTag { PersonId = person.Id, TagId = tag1.Id },
            new PersonTag { PersonId = person.Id, TagId = tag2.Id }
        };
        await _repository.AddRangeAsync(personTags);
        await _tagRepository.DeleteAsync(tag1);
        var result = await _repository.GetAllAsync();
        Assert.That(result, Has.Count.EqualTo(1));
    }
}