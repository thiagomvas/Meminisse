using Avalonia;
using Avalonia.Controls;
using Meminisse.Models;
using Meminisse.ViewModels.Components;

namespace Meminisse.Views.Components;

public partial class MemoryView : UserControl
{
    public static readonly StyledProperty<MemoryDto> MemoryProperty = AvaloniaProperty.Register<MemoryView, MemoryDto>(nameof(MemoryDto));

    public MemoryDto Memory
    {
        get => GetValue(MemoryProperty);
        set
        {
            SetValue(MemoryProperty, value);
            DataContext = new MemoryViewModel(value);
        }
    }

    public MemoryView()
    {
        InitializeComponent();
    }
}