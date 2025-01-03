using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meminisse.Core.Entities;
public class MemoryPerson
{
    public ulong MemoryId { get; set; }
    public Memory Memory { get; set; }
    public ulong PersonId { get; set; }
    public Person Person { get; set; }
}
