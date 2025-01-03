using Meminisse.Core.Abstractions;
using Meminisse.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        _context.People.Add(entity);
        await _context.SaveChangesAsync();
    }

    public async Task AddRangeAsync(List<Person> entities)
    {
        _context.People.AddRange(entities);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Person entity)
    {
        _context.People.Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ulong id)
    {
        _context.People.Remove(_context.People.Find(id));
        await _context.SaveChangesAsync();
    }

    public IQueryable<Person> Get()
    {
        return _context.People;
    }

    public async Task<List<Person>> GetAllAsync()
    {
        return await _context.People.ToListAsync();
    }

    public async Task<Person?> GetAsync(ulong id)
    {
        return await _context.People.FindAsync(id);
    }

    public async Task UpdateAsync(Person entity)
    {
        _context.People.Update(entity);
        await _context.SaveChangesAsync();
    }
}
