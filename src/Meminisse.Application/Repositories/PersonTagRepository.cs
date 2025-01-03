using Meminisse.Core.Abstractions;
using Meminisse.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meminisse.Application.Repositories;
public class PersonTagRepository : IRepository<PersonTag>
{
    private readonly MeminisseDbContext _context;

    public PersonTagRepository(MeminisseDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(PersonTag entity)
    {
        await _context.PersonTags.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(List<PersonTag> entities)
    {
        await _context.PersonTags.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(PersonTag entity)
    {
        _context.PersonTags.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ulong id)
    {
        var entity = await _context.PersonTags.FindAsync(id);
        if (entity != null)
        {
            _context.PersonTags.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public IQueryable<PersonTag> Get()
    {
        return _context.PersonTags.AsQueryable();
    }

    public async Task<List<PersonTag>> GetAllAsync()
    {
        return await _context.PersonTags.ToListAsync();
    }

    public async Task<PersonTag?> GetAsync(ulong id)
    {
        return await _context.PersonTags.FindAsync(id);
    }

    public async Task UpdateAsync(PersonTag entity)
    {
        _context.PersonTags.Update(entity);
        await _context.SaveChangesAsync();
    }
}
