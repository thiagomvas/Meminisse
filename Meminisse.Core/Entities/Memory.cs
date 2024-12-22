using Meminisse.Core.ValueTypes;

namespace Meminisse.Core.Entities;
public class Memory
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = "Untitled";
    public string Description { get; set; } = "Description";
    public DateTime Date { get; set; } = DateTime.Now;
    public Emotion Emotion { get; set; } = Emotion.Neutral;

    public ICollection<MemoryPerson> MemoryPeople { get; set; } = [];  // Many-to-many relationship
    public ICollection<MemoryItem> MemoryItems { get; set; } = [];
    public ICollection<MemoryTag> MemoryTags { get; set; } = [];

}
