using Meminisse.Core.Entities;

namespace Meminisse.Core.Abstractions;
public interface IPersonService
{
    Task<Person> GetPersonByIdAsync(Guid id);
    Task<IEnumerable<Person>> GetPeopleByTagAsync(Guid tagId);
}
