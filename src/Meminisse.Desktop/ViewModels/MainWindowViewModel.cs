using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Material.Icons;
using Material.Icons.Avalonia;
using Meminisse.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Meminisse.Desktop.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty] private ViewModelBase? _selectedViewModel;
    [ObservableProperty] private MaterialIconKind _sideBarIcon = MaterialIconKind.Menu;
    [ObservableProperty] private bool _sidebarCollapsed = false;
    [ObservableProperty] private double _sidebarWidth = 250;

    private readonly IServiceProvider _provider;
    public ObservableCollection<NavigationItem> NavigationItems { get; } 

    public MainWindowViewModel(IServiceProvider provider)
    {
        _provider = provider;
        NavigationItems = new ObservableCollection<NavigationItem>
        {
            new NavigationItem("Home", _provider.GetRequiredService<HomeViewModel>(), MaterialIconKind.Home),
            new NavigationItem("Settings", _provider.GetRequiredService<HomeViewModel>(), MaterialIconKind.Settings)
        };
        SelectedViewModel = NavigationItems.FirstOrDefault()!.ViewModel;
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
        using (var scope = _provider.CreateScope())
        {
            SelectedViewModel = scope.ServiceProvider.GetRequiredService(viewModel.GetType()) as ViewModelBase;
        }
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