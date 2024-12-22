namespace Meminisse.Core.Entities;
public class MemoryTag
{
    public Guid MemoryId { get; set; }
    public Memory Memory { get; set; }
    public Guid TagId { get; set; }
    public Tag Tag { get; set; }
}
