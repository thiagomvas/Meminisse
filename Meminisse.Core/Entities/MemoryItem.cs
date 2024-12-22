using Meminisse.Core.ValueTypes;

namespace Meminisse.Core.Entities;
public class MemoryItem
{
    public Guid Id { get; set; }
    public Guid MemoryId { get; set; }
    public string Content { get; set; }
    public DateTime DateAdded { get; set; }
    public MemoryType Type { get; set; }

    public Memory Memory { get; set; }
}
