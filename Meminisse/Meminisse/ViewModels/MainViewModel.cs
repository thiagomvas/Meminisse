using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System.Windows.Input;

namespace Meminisse.ViewModels;

internal class MainViewModel : ViewModelBase
{
#pragma warning disable CA1822 // Mark members as static
    public string Greeting => "Welcome to Avalonia!";
#pragma warning restore CA1822 // Mark members as static
    private readonly MainWindowViewModel _parent;
    public ICommand NavigateToHome { get; }

    public MainViewModel(MainWindowViewModel parent)
    {
        _parent = parent;
        NavigateToHome = ReactiveCommand.Create(() => _parent.NavigateTo(App.ServiceProvider.GetRequiredService<HomeViewModel>()));
    }

}

