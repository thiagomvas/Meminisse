using Meminisse.Models;
using ReactiveUI;
using System;

namespace Meminisse.ViewModels.Components;
public class MemoryViewModel : ViewModelBase
{
    private MemoryDto _memory;

    public MemoryDto Memory
    {
        get => _memory;
        set => this.RaiseAndSetIfChanged(ref _memory, value);
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }

    public MemoryViewModel(MemoryDto memory)
    {
        Memory = memory ?? throw new ArgumentNullException(nameof(memory));
    }
}
