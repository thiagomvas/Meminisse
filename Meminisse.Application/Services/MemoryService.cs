using Meminisse.Core.Abstractions;
using Meminisse.Core.Entities;
using Meminisse.Core.ValueTypes;
using Microsoft.EntityFrameworkCore;

namespace Meminisse.Application.Services;
public class MemoryService : IMemoryService
{
    private readonly IRepository<Memory> _memoryRepository;
    private readonly IRepository<Person> _personRepository;
    private readonly IRepository<Tag> _tagRepository;
    private readonly IRepository<MemoryPerson> _memoryPersonRepository;
    private readonly IRepository<MemoryTag> _memoryTagRepository;


    public MemoryService(IRepository<Memory> memoryRepository, IRepository<Person> personRepository, IRepository<Tag> tagRepository, IRepository<MemoryPerson> memoryPersonRepository, IRepository<MemoryTag> memoryTagRepository)
    {
        _memoryRepository = memoryRepository;
        _personRepository = personRepository;
        _tagRepository = tagRepository;
        _memoryPersonRepository = memoryPersonRepository;
        _memoryTagRepository = memoryTagRepository;
    }


    public async Task AddPersonAsync(Guid memoryId, Guid personId)
    {
        var memory = await _memoryRepository.Get().Where(m => m.Id == memoryId).FirstOrDefaultAsync();
        var person = await _personRepository.Get().Where(p => p.Id == personId).FirstOrDefaultAsync();
        if (memory == null)
        {
            throw new Exception("Memory not found");
        }
        else if (person == null)
        {
            throw new Exception("Person not found");
        }

        var memoryPerson = new MemoryPerson
        {
            MemoryId = memoryId,
            PersonId = personId
        };

        await _memoryPersonRepository.AddAsync(memoryPerson);
    }

    public async Task AddTagAsync(Guid memoryId, Guid tagId)
    {
        var memory = await _memoryRepository.Get().Where(m => m.Id == memoryId).FirstOrDefaultAsync();
        var tag = await _tagRepository.Get().Where(t => t.Id == tagId).FirstOrDefaultAsync();
        if (memory == null)
        {
            throw new Exception("Memory not found");
        }
        else if (tag == null)
        {
            throw new Exception("Tag not found");
        }

        var memoryTag = new MemoryTag
        {
            MemoryId = memoryId,
            TagId = tagId
        };

        await _memoryTagRepository.AddAsync(memoryTag);
    }
    public async Task<IEnumerable<Memory>> GetByPersonAsync(Guid personId)
    {
        var memoryPeople = await _memoryPersonRepository
            .Get()
            .Where(mp => mp.PersonId == personId)
            .ToListAsync();

        var memoryIds = memoryPeople.Select(mp => mp.MemoryId).Distinct();

        return await _memoryRepository
            .Get()
            .Where(m => memoryIds.Contains(m.Id))
            .ToListAsync();
    }

    public async Task<IEnumerable<Memory>> GetInCommonAsync(IEnumerable<Guid> people)
    {
        var memoryIds = await _memoryPersonRepository
            .Get()
            .Where(mp => people.Contains(mp.PersonId))
            .GroupBy(mp => mp.MemoryId)
            .Where(g => g.Count() == people.Count())
            .Select(g => g.Key)
            .ToListAsync();

        return await _memoryRepository
            .Get()
            .Where(m => memoryIds.Contains(m.Id))
            .ToListAsync();
    }

    public async Task<IEnumerable<Memory>> GetByEmotionAsync(Emotion emotion)
    {
        return await _memoryRepository
            .Get()
            .Where(m => (m.Emotion & emotion) == emotion)
            .ToListAsync();
    }

    public async Task RemovePersonAsync(Guid memoryId, Guid personId)
    {
        var memoryPerson = await _memoryPersonRepository
            .Get()
            .Where(mp => mp.MemoryId == memoryId && mp.PersonId == personId)
            .FirstOrDefaultAsync();

        if (memoryPerson == null)
            return;

        await _memoryPersonRepository.DeleteAsync(memoryPerson);
    }

    public async Task RemoveTagAsync(Guid memoryId, Guid tagId)
    {
        var memoryTag = await _memoryTagRepository
            .Get()
            .Where(mt => mt.MemoryId == memoryId && mt.TagId == tagId)
            .FirstOrDefaultAsync();

        if (memoryTag == null)
            return;

        await _memoryTagRepository.DeleteAsync(memoryTag);
    }

    public async Task<Memory?> GetByIdAsync(Guid id)
    {
        return await _memoryRepository.Get().Where(m => m.Id == id).FirstOrDefaultAsync();
    }
}
