using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meminisse.Core.Entities;
public class Tag
{
    public ulong Id { get; set; }
    public string Name { get; set; }

    public ICollection<MemoryTag> MemoryTags { get; set; } = [];
    public ICollection<PersonTag> PersonTags { get; set; } = [];
}
