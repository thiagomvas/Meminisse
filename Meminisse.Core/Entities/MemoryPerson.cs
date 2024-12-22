namespace Meminisse.Core.Entities;
public class MemoryPerson
{
    public Guid MemoryId { get; set; }
    public Memory Memory { get; set; }
    public Guid PersonId { get; set; }
    public Person Person { get; set; }
}
