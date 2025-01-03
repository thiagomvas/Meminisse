namespace Meminisse.Core.Abstractions;
public interface IRepository<T> where T : class
{
    Task AddAsync(T entity);
    Task AddRangeAsync(List<T> entities);
    Task DeleteAsync(T entity);
    Task DeleteAsync(ulong id);
    Task<T?> GetAsync(ulong id);
    Task<List<T>> GetAllAsync();
    IQueryable<T> Get();
    Task UpdateAsync(T entity);
}