namespace Meminisse.Core.Entities;
public class PersonTag
{
    public Guid PersonId { get; set; }
    public Person Person { get; set; }
    public Guid TagId { get; set; }
    public Tag Tag { get; set; }
}
