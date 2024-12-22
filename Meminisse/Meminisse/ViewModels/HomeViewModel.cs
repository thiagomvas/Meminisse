using Meminisse.Application;
using Meminisse.Core.Entities;
using Meminisse.Models;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Meminisse.ViewModels;
internal class HomeViewModel : ViewModelBase
{
    private readonly MainWindowViewModel _parent;
    private readonly MeminisseDbContext _context;
    public ICommand NavigateToMain { get; }
    public ICommand CreateNewMemory { get; }

    public ObservableCollection<MemoryDto> Memories { get; set; } = new();

    public HomeViewModel(MainWindowViewModel parent, MeminisseDbContext context)
    {
        _context = context;
        _parent = parent;
        NavigateToMain = ReactiveCommand.Create(() => _parent.NavigateTo(App.ServiceProvider.GetRequiredService<MainViewModel>()));

        foreach (var memory in _context.Memories)
        {
            var dto = new MemoryDto()
            {
                Title = memory.Title,
                Description = memory.Description,
                Date = memory.Date
            };
            Memories.Add(dto);
        }


        CreateNewMemory = ReactiveCommand.Create(async () =>
        {
            var memory = new Memory() { Title = "Untitled", Description = "Description" };
            await _context.Memories.AddAsync(memory);
            await _context.SaveChangesAsync();
            var dto = new MemoryDto()
            {
                Title = memory.Title,
                Description = memory.Description,
                Date = memory.Date
            };
            Memories.Add(dto);
        });

    }

}
