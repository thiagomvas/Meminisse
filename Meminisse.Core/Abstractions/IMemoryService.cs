using Meminisse.Core.Entities;
using Meminisse.Core.ValueTypes;

namespace Meminisse.Core.Abstractions;
public interface IMemoryService
{
    Task AddPersonAsync(Guid memoryId, Guid personId);
    Task AddTagAsync(Guid memoryId, Guid tagId);
    Task<Memory?> GetByIdAsync(Guid id);
    Task<IEnumerable<Memory>> GetByPersonAsync(Guid personId);
    Task<IEnumerable<Memory>> GetInCommonAsync(IEnumerable<Guid> people);
    Task<IEnumerable<Memory>> GetByEmotionAsync(Emotion emotion);
    Task RemoveTagAsync(Guid memoryId, Guid tagId);
    Task RemovePersonAsync(Guid memoryId, Guid tagId);
}
