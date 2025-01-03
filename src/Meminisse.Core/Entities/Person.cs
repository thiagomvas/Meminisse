using Meminisse.Core.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Meminisse.Core.Entities;
public class Person
{
    public ulong Id { get; set; }
    public string FullName { get; set; } = "";
    public string Bio { get; set; } = "";
    public string ProfilePictureUrl { get; set; } = "";
    public DateTime? Birthday { get; set; } = null;
    public Gender Gender { get; set; } = Gender.Other;


    public ICollection<MemoryPerson> MemoryPeople { get; set; } = [];  // Many-to-many relationship
    public ICollection<PersonTag> PersonTags { get; set; } = [];
    public ICollection<Memory> Memories => MemoryPeople.Select(mp => mp.Memory).ToList();
}
