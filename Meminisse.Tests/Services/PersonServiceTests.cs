using Meminisse.Application.Repositories;
using Meminisse.Application.Services;
using Meminisse.Core.Abstractions;
using Meminisse.Core.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Meminisse.Tests.Services;
internal class PersonServiceTests : BaseServiceTest
{
    private PersonService _personService;
    private IRepository<Person> _personRepository;
    private IRepository<Tag> _tagRepository;
    private IRepository<PersonTag> _personTagRepository;

    public override void SetUp()
    {
        Services.AddScoped<PersonService>();
        Services.AddScoped<IRepository<Person>, PersonRepository>();
        Services.AddScoped<IRepository<Tag>, TagRepository>();
        Services.AddScoped<IRepository<PersonTag>, PersonTagRepository>();

        base.SetUp();
        _personService = Provider.GetRequiredService<PersonService>();
        _personRepository = Provider.GetRequiredService<IRepository<Person>>();
        _tagRepository = Provider.GetRequiredService<IRepository<Tag>>();
        _personTagRepository = Provider.GetRequiredService<IRepository<PersonTag>>();
    }

    [Test]
    public async Task GetPeopleByTagAsync_ShouldReturnPeopleWithTag()
    {
        var tag = new Tag { Id = Guid.NewGuid(), Name = "Test Tag" };
        var person1 = new Person { Id = Guid.NewGuid(), FullName = "Person 1" };
        var person2 = new Person { Id = Guid.NewGuid(), FullName = "Person 2" };
        var personTag1 = new PersonTag { TagId = tag.Id, PersonId = person1.Id, Person = person1 };
        var personTag2 = new PersonTag { TagId = tag.Id, PersonId = person2.Id, Person = person2 };

        await _tagRepository.AddAsync(tag);
        await _personRepository.AddAsync(person1);
        await _personRepository.AddAsync(person2);
        await _personTagRepository.AddAsync(personTag1);
        await _personTagRepository.AddAsync(personTag2);

        var result = await _personService.GetPeopleByTagAsync(tag.Id);

        Assert.That(result, Has.Exactly(2).Items);
        Assert.That(result.Any(p => p.Id == person1.Id), Is.True);
        Assert.That(result.Any(p => p.Id == person2.Id), Is.True);
    }

    [Test]
    public void GetPeopleByTagAsync_ShouldThrowException_WhenTagDoesNotExist()
    {
        var nonExistentTagId = Guid.NewGuid();

        Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await _personService.GetPeopleByTagAsync(nonExistentTagId);
        });
    }

    [Test]
    public async Task GetPersonByIdAsync_ShouldReturnPersonWithId()
    {
        var person = new Person { Id = Guid.NewGuid(), FullName = "Test Person" };

        await _personRepository.AddAsync(person);

        var result = await _personService.GetPersonByIdAsync(person.Id);

        Assert.That(result, Is.Not.Null);
        Assert.That(result.Id, Is.EqualTo(person.Id));
        Assert.That(result.FullName, Is.EqualTo(person.FullName));
    }

    [Test]
    public async Task GetPersonByIdAsync_ShouldReturnNull_WhenPersonDoesNotExist()
    {
        var nonExistentPersonId = Guid.NewGuid();

        var result = await _personService.GetPersonByIdAsync(nonExistentPersonId);

        Assert.That(result, Is.Null);
    }
}

