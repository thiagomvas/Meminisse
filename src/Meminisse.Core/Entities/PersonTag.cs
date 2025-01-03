using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meminisse.Core.Entities;
public class PersonTag
{
    public ulong PersonId { get; set; }
    public Person Person { get; set; }
    public ulong TagId { get; set; }
    public Tag Tag { get; set; }
}
