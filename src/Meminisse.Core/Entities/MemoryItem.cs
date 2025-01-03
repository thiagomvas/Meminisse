using Meminisse.Core.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meminisse.Core.Entities;
public class MemoryItem
{
    public ulong Id { get; set; }
    public ulong MemoryId { get; set; }
    public string Content { get; set; }
    public DateTime DateAdded { get; set; }
    public MemoryType Type { get; set; }

    public Memory Memory { get; set; }
}
