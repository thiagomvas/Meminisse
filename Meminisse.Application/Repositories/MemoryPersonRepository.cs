using Meminisse.Core.Abstractions;
using Meminisse.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meminisse.Application.Repositories;
public class MemoryPersonRepository : IRepository<MemoryPerson>
{
    private readonly MeminisseDbContext _context;

    public MemoryPersonRepository(MeminisseDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(MemoryPerson entity)
    {
        await _context.MemoryPeople.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(List<MemoryPerson> entities)
    {
        await _context.MemoryPeople.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(MemoryPerson entity)
    {
        _context.MemoryPeople.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        throw new InvalidOperationException("MemoryPerson uses a 2 part key, cannot run delete operation with only one Guid");
    }

    public IQueryable<MemoryPerson> Get()
    {
        return _context.MemoryPeople.AsQueryable();
    }

    public async Task<List<MemoryPerson>> GetAllAsync()
    {
        return await _context.MemoryPeople.ToListAsync();
    }

    public async Task UpdateAsync(MemoryPerson entity)
    {
        _context.MemoryPeople.Update(entity);
        await _context.SaveChangesAsync();
    }
}
