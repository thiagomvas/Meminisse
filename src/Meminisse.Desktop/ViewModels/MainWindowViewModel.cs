using System.Collections.ObjectModel;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;
using Material.Icons.Avalonia;

namespace Meminisse.Desktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private ViewModelBase? _selectedViewModel = new HomeViewModel();
    [ObservableProperty] private MaterialIconKind _sideBarIcon = MaterialIconKind.Menu;
    [ObservableProperty] private bool _sidebarCollapsed = false;
    [ObservableProperty] private double _sidebarWidth = 250;
    
    public ObservableCollection<NavigationItem> NavigationItems { get; } = new ObservableCollection<NavigationItem>
    {
        new NavigationItem("Home", new HomeViewModel(), MaterialIconKind.Home),
        new NavigationItem("Settings", new SettingsViewModel(), MaterialIconKind.Settings)
    };

    public MainWindowViewModel()
    {
        ToggleSidebar();
        
    }
    [RelayCommand]
    public void ToggleSidebar()
    {
        SidebarCollapsed = !SidebarCollapsed;
        SideBarIcon = SidebarCollapsed ? MaterialIconKind.MenuDown : MaterialIconKind.Menu;
        SidebarWidth = SidebarCollapsed ? 250 : 50;
    }
    
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
    public MaterialIconKind Icon { get; set; }
    
    public NavigationItem(string name, ViewModelBase viewModel, MaterialIconKind icon)
    {
        Name = name;
        ViewModel = viewModel;
        Icon = icon;
    }
}