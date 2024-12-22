using Meminisse.Core.Abstractions;
using Meminisse.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meminisse.Application.Repositories;
public class MemoryRepository : IRepository<Memory>
{
    private readonly MeminisseDbContext _context;

    public MemoryRepository(MeminisseDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Memory entity)
    {
        await _context.Memories.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(List<Memory> entities)
    {
        await _context.Memories.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Memory entity)
    {
        _context.Memories.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _context.Memories.FindAsync(id);
        if (entity != null)
        {
            _context.Memories.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public IQueryable<Memory> Get()
    {
        return _context.Memories.AsQueryable();
    }

    public async Task<List<Memory>> GetAllAsync()
    {
        return await _context.Memories.ToListAsync();
    }

    public async Task UpdateAsync(Memory entity)
    {
        _context.Memories.Update(entity);
        await _context.SaveChangesAsync();
    }
}
