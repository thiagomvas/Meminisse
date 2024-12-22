namespace Meminisse.Core.Entities;
public class Tag
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }

    public ICollection<MemoryTag> MemoryTags { get; set; } = [];
    public ICollection<PersonTag> PersonTags { get; set; } = [];
}
