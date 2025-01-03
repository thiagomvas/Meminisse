using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meminisse.Core.Entities;
public class MemoryTag
{
    public ulong MemoryId { get; set; }
    public Memory Memory { get; set; }
    public ulong TagId { get; set; }
    public Tag Tag { get; set; }
}
