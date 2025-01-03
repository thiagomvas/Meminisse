using Meminisse.Core.Abstractions;
using Meminisse.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        _context.MemoryPeople.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(List<MemoryPerson> entities)
    {
        _context.MemoryPeople.AddRange(entities);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(MemoryPerson entity)
    {

        _context.MemoryPeople.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ulong id)
    {
        _context.MemoryPeople.Remove(_context.MemoryPeople.Find(id));
        await _context.SaveChangesAsync();
    }

    public IQueryable<MemoryPerson> Get()
    {
        return _context.MemoryPeople;
    }

    public async Task<List<MemoryPerson>> GetAllAsync()
    {
        return await _context.MemoryPeople.ToListAsync();
    }

    public async Task<MemoryPerson?> GetAsync(ulong id)
    {
        return await _context.MemoryPeople.FindAsync(id);
    }

    public async Task UpdateAsync(MemoryPerson entity)
    {
        _context.MemoryPeople.Update(entity);
        await _context.SaveChangesAsync();
    }
}
