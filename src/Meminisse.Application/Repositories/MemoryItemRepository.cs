using Meminisse.Core.Abstractions;
using Meminisse.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Meminisse.Application.Repositories;
public class MemoryItemRepository : IRepository<MemoryItem>
{
    private readonly MeminisseDbContext _context;

    public MemoryItemRepository(MeminisseDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(MemoryItem entity)
    {
        await _context.MemoryItems.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(List<MemoryItem> entities)
    {
        await _context.MemoryItems.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(MemoryItem entity)
    {
        _context.MemoryItems.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ulong id)
    {
        var entity = await _context.MemoryItems.FindAsync(id);
        if (entity != null)
        {
            _context.MemoryItems.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public IQueryable<MemoryItem> Get()
    {
        return _context.MemoryItems.AsQueryable();
    }

    public async Task<List<MemoryItem>> GetAllAsync()
    {
        return await _context.MemoryItems.ToListAsync();
    }

    public async Task<MemoryItem?> GetAsync(ulong id)
    {
        return await _context.MemoryItems.FindAsync(id);
    }

    public async Task UpdateAsync(MemoryItem entity)
    {
        _context.MemoryItems.Update(entity);
        await _context.SaveChangesAsync();
    }
}