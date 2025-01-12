using CommunityToolkit.Mvvm.ComponentModel;

namespace Meminisse.Desktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private ViewModelBase? _selectedViewModel = new HomeViewModel();
}