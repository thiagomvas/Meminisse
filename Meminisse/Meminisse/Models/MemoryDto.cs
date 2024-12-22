using Avalonia;
using System;

namespace Meminisse.Models;
public class MemoryDto : AvaloniaObject
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = "Unnamed Memory";
    public string Description { get; set; } = "No description";
    public DateTime Date { get; set; } = DateTime.Now;

    public override string ToString()
    {
        return $"{Title} - {Date}";
    }
}
