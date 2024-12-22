using Meminisse.Application;
using Meminisse.Application.Repositories;
using Meminisse.Application.Services;
using Meminisse.Core.Entities;
using Meminisse.Core.ValueTypes;
using Microsoft.EntityFrameworkCore;

using var context = new MeminisseDbContext();
await context.Database.EnsureDeletedAsync();
await context.Database.EnsureCreatedAsync();

var personRepo = new PersonRepository(context);
var memoryRepo = new MemoryRepository(context);
var memoryItemRepo = new MemoryItemRepository(context);
var tagRepo = new TagRepository(context);
var memoryPersonRepo = new MemoryPersonRepository(context);
var personTagRepo = new PersonTagRepository(context);
var memoryTagRepo = new MemoryTagRepository(context);
var memoryService = new MemoryService(memoryRepo, personRepo, tagRepo, memoryPersonRepo, memoryTagRepo);

var person = new Person
{
    FullName = "John Doe",
};

var memory = new Memory
{
    Title = "Test Memory",
    Description = "Test Description",
    Date = DateTime.Now,
    Emotion = Emotion.Joy,
};

await personRepo.AddAsync(person);
await memoryRepo.AddAsync(memory);

var memoryPerson = new MemoryPerson
{
    MemoryId = memory.Id,
    PersonId = person.Id,
};

await memoryPersonRepo.AddAsync(memoryPerson);

var result = await memoryPersonRepo.GetAllAsync();
Console.WriteLine(result.Count); // Output: 1

memory = await memoryRepo.Get().Include(m => m.MemoryPeople).FirstOrDefaultAsync();

await memoryRepo.DeleteAsync(memory);

result = await memoryPersonRepo.GetAllAsync();

Console.WriteLine(result.Count); // Output: 0