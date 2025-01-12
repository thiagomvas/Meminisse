using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Meminisse.Desktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private ViewModelBase? _selectedViewModel = new HomeViewModel();
    
    public ObservableCollection<NavigationItem> NavigationItems { get; } = new ObservableCollection<NavigationItem>
    {
        new NavigationItem("Home", new HomeViewModel()),
        new NavigationItem("Settings", new SettingsViewModel())
    };
    
    [RelayCommand]
    public void SelectViewModel(ViewModelBase viewModel)
    {
        SelectedViewModel = viewModel;
    }
}

public class NavigationItem
{
    public string Name { get; set; }
    public ViewModelBase ViewModel { get; set; }
    
    public NavigationItem(string name, ViewModelBase viewModel)
    {
        Name = name;
        ViewModel = viewModel;
    }
}