using Meminisse.Application.Repositories;
using Meminisse.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meminisse.Tests.Repositories;

[TestFixture]
internal class PersonRepositoryTests : BaseRepositoryTest
{
    private PersonRepository _repository;

    public override void SetUp()
    {
        base.SetUp();
        _repository = new PersonRepository(Context);
    }

    [Test]
    public async Task AddAsync_ShouldAddPerson()
    {
        var person = new Person { FullName = "John Doe" };

        await _repository.AddAsync(person);
        var result = await Context.People.FindAsync(person.Id);

        Assert.NotNull(result);
        Assert.That(result.FullName, Is.EqualTo(person.FullName));
    }

    [Test]
    public async Task AddRangeAsync_ShouldAddPeople()
    {
        var people = new List<Person>
        {
            new Person {   FullName = "John Doe" },
            new Person {   FullName = "Jane Doe" }
        };

        await _repository.AddRangeAsync(people);
        var result = await Context.People.ToListAsync();

        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task DeleteAsync_ShouldDeletePerson()
    {
        var person = new Person { FullName = "John Doe" };
        await Context.People.AddAsync(person);
        await Context.SaveChangesAsync();

        await _repository.DeleteAsync(person);
        var result = await Context.People.FindAsync(person.Id);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task DeleteAsync_ById_ShouldDeletePerson()
    {
        var person = new Person { FullName = "John Doe" };
        await Context.People.AddAsync(person);
        await Context.SaveChangesAsync();

        await _repository.DeleteAsync(person.Id);
        var result = await Context.People.FindAsync(person.Id);

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task Get_ShouldReturnQueryable()
    {
        var person = new Person { FullName = "John Doe" };
        await Context.People.AddAsync(person);
        await Context.SaveChangesAsync();

        var result = _repository.Get().ToList();

        Assert.That(result, Has.Count.EqualTo(1));
    }

    [Test]
    public async Task GetAllAsync_ShouldReturnAllPeople()
    {
        var people = new List<Person>
        {
            new Person {   FullName = "John Doe" },
            new Person {   FullName = "Jane Doe" }
        };
        await Context.People.AddRangeAsync(people);
        await Context.SaveChangesAsync();

        var result = await _repository.GetAllAsync();

        Assert.That(result, Has.Count.EqualTo(2));
    }

    [Test]
    public async Task UpdateAsync_ShouldUpdatePerson()
    {
        var person = new Person { FullName = "John Doe" };
        await Context.People.AddAsync(person);
        await Context.SaveChangesAsync();

        person.FullName = "John Smith";
        await _repository.UpdateAsync(person);
        var result = await Context.People.FindAsync(person.Id);

        Assert.That(result.FullName, Is.EqualTo("John Smith"));
    }
}