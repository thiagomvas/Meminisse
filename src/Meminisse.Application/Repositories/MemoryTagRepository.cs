using Meminisse.Core.Abstractions;
using Meminisse.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meminisse.Application.Repositories;
public class MemoryTagRepository : IRepository<MemoryTag>
{
    private readonly MeminisseDbContext _context;

    public MemoryTagRepository(MeminisseDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(MemoryTag entity)
    {
        await _context.MemoryTags.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(List<MemoryTag> entities)
    {
        await _context.MemoryTags.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(MemoryTag entity)
    {
        _context.MemoryTags.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ulong id)
    {
        var entity = await _context.MemoryTags.FindAsync(id);
        if (entity != null)
        {
            _context.MemoryTags.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public IQueryable<MemoryTag> Get()
    {
        return _context.MemoryTags.AsQueryable();
    }

    public async Task<List<MemoryTag>> GetAllAsync()
    {
        return await _context.MemoryTags.ToListAsync();
    }

    public async Task<MemoryTag?> GetAsync(ulong id)
    {
        return await _context.MemoryTags.FindAsync(id);
    }

    public async Task UpdateAsync(MemoryTag entity)
    {
        _context.MemoryTags.Update(entity);
        await _context.SaveChangesAsync();
    }
}
