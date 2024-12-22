using Meminisse.Core.Abstractions;
using Meminisse.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meminisse.Application.Services;
public class PersonService : IPersonService
{
    private readonly IRepository<Person> _personRepository;
    private readonly IRepository<Tag> _tagRepository;
    private readonly IRepository<PersonTag> _personTagRepository;

    public PersonService(IRepository<Person> personRepository, IRepository<Tag> tagRepository, IRepository<PersonTag> personTagRepository)
    {
        _personRepository = personRepository;
        _tagRepository = tagRepository;
        _personTagRepository = personTagRepository;
    }

    public async Task<IEnumerable<Person>> GetPeopleByTagAsync(Guid tagId)
    {
        var tag = await _tagRepository.Get().FirstOrDefaultAsync(t => t.Id == tagId) ?? throw new ArgumentException("Tag does not exist");
        var people = await _personTagRepository.Get().Where(pt => pt.TagId == tagId).Select(pt => pt.Person).ToListAsync();
        return people;
    }

    public async Task<Person> GetPersonByIdAsync(Guid id)
    {
        return await _personRepository.Get().FirstOrDefaultAsync(p => p.Id == id);
    }

}
