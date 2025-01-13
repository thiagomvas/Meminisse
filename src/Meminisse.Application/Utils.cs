using Bogus;
using Meminisse.Core.Abstractions;
using Meminisse.Core.Entities;
using Meminisse.Core.ValueTypes;
using Person = Meminisse.Core.Entities.Person;

namespace Meminisse.Application;

public class Utils
{
    public static async Task PopulatePeopleAsync(IRepository<Person> personRepo, int amount)
    {
        var faker = new Faker<Person>()
            .RuleFor(p => p.FullName, f => f.Name.FullName())
            .RuleFor(p => p.Bio, f => f.Lorem.Paragraph())
            .RuleFor(p => p.ProfilePictureUrl, f => f.Internet.Url())
            .RuleFor(p => p.Birthday, f => f.Date.Past(30))
            .RuleFor(p => p.Gender, f => f.PickRandom<Gender>());
        
        var people = faker.Generate(amount);
        await personRepo.AddRangeAsync(people);
    }
    
    public static async Task PopulateMemoriesAsync(IRepository<Memory> memoryRepo, IRepository<Person> personRepo, int amount)
    {
        var faker2 = new Faker<MemoryItem>()
            .RuleFor(m => m.Content, f => f.Lorem.Paragraph())
            .RuleFor(m => m.DateAdded, f => f.Date.Past(1))
            .RuleFor(m => m.Type, f => f.PickRandom<MemoryType>());
        var faker = new Faker<Memory>()
            .RuleFor(m => m.Title, f => f.Lorem.Sentence())
            .RuleFor(m => m.Description, f => f.Lorem.Paragraph())
            .RuleFor(m => m.Date, f => f.Date.Past(1))
            .RuleFor(m => m.Emotion, f => f.PickRandom<Emotion>())
            .RuleFor(m => m.MemoryItems, f => faker2.Generate(3));
        
        
        var memories = faker.Generate(amount);
        
        // Pick random people and assign to the memories
        foreach (var memory in memories)
        {
            var people = await personRepo.GetAllAsync();
            var randomPeople = people.OrderBy(p => Guid.NewGuid()).Take(3).ToList();
            foreach (var person in randomPeople)
            {
                memory.MemoryPeople.Add(new MemoryPerson
                {
                    Memory = memory,
                    Person = person
                });
            }
        }
        
        await memoryRepo.AddRangeAsync(memories);
    }
}