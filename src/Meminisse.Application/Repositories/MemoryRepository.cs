using Meminisse.Core.Abstractions;
using Meminisse.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        _context.Memories.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(List<Memory> entities)
    {
        _context.Memories.AddRange(entities);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Memory entity)
    {
        /*
         * Manually removing related MemoryPeople due to an unknown issue that couldn't be fixed.
         * The error is thrown in this method.
         * 
         * Message: 
            System.ArgumentNullException : Value cannot be null. (Parameter 'key')
         */
        var memory = await _context.Memories
            .Include(m => m.MemoryPeople)
            .FirstOrDefaultAsync(m => m.Id == entity.Id);

        if (memory != null)
        {
            _context.MemoryPeople.RemoveRange(memory.MemoryPeople); // Manually remove related entities
            _context.Memories.Remove(memory);
            await _context.SaveChangesAsync();
        }
    }


    public async Task DeleteAsync(ulong id)
    {
        var target = await _context.Memories.FindAsync(id);
        if(target != null)
            await DeleteAsync(target);
    }

    public IQueryable<Memory> Get()
    {
        return _context.Memories;
    }

    public async Task<List<Memory>> GetAllAsync()
    {
        return await _context.Memories.ToListAsync();
    }

    public async Task<Memory?> GetAsync(ulong id)
    {
        return await _context.Memories.FindAsync(id);
    }

    public async Task UpdateAsync(Memory entity)
    {
        _context.Memories.Update(entity);
        await _context.SaveChangesAsync();
    }
}
