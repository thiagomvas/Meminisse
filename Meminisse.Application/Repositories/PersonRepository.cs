using Meminisse.Core.Abstractions;
using Meminisse.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meminisse.Application.Repositories;
public class PersonRepository : IRepository<Person>
{
    private readonly MeminisseDbContext _context;

    public PersonRepository(MeminisseDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Person entity)
    {
        await _context.People.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(List<Person> entities)
    {
        await _context.People.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Person entity)
    {
        _context.People.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.People.FindAsync(id);
        if (entity != null)
        {
            _context.People.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public IQueryable<Person> Get()
    {
        return _context.People.AsQueryable();
    }

    public async Task<List<Person>> GetAllAsync()
    {
        return await _context.People.ToListAsync();
    }

    public async Task UpdateAsync(Person entity)
    {
        _context.People.Update(entity);
        await _context.SaveChangesAsync();
    }
}
