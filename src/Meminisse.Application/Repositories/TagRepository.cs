using Meminisse.Core.Abstractions;
using Meminisse.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meminisse.Application.Repositories;
public class TagRepository : IRepository<Tag>
{
    private readonly MeminisseDbContext _context;

    public TagRepository(MeminisseDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Tag entity)
    {
        await _context.Tags.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(List<Tag> entities)
    {
        await _context.Tags.AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Tag entity)
    {
        _context.Tags.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ulong id)
    {
        var entity = await _context.Tags.FindAsync(id);
        if (entity != null)
        {
            _context.Tags.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    public IQueryable<Tag> Get()
    {
        return _context.Tags.AsQueryable();
    }

    public async Task<List<Tag>> GetAllAsync()
    {
        return await _context.Tags.ToListAsync();
    }

    public async Task<Tag?> GetAsync(ulong id)
    {
        return await _context.Tags.FindAsync(id);
    }

    public async Task UpdateAsync(Tag entity)
    {
        _context.Tags.Update(entity);
        await _context.SaveChangesAsync();
    }
}
