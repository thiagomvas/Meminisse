using ReactiveUI;

namespace Meminisse.ViewModels;
internal class MainWindowViewModel : ViewModelBase
{
    private ViewModelBase _currentView;
    public ViewModelBase CurrentView
    {
        get => _currentView;
        set => this.RaiseAndSetIfChanged(ref _currentView, value);
    }

    public MainWindowViewModel()
    {
    }

    public void NavigateTo(ViewModelBase viewModel)
    {
        CurrentView = viewModel;
    }

}
