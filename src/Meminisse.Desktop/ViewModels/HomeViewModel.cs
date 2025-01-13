using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;
using Meminisse.Application;
using Meminisse.Core.Abstractions;
using Meminisse.Core.Entities;

namespace Meminisse.Desktop.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    private readonly IRepository<Memory> _memoryRepo;
    private readonly IRepository<Person> _personRepo;
    
    public ObservableCollection<Person> People { get; }
    public ObservableCollection<Memory> Memories { get; }
    public HomeViewModel(IRepository<Memory> memoryRepo, IRepository<Person> personRepo)
    {
        _memoryRepo = memoryRepo;
        _personRepo = personRepo;

        Utils.PopulatePeopleAsync(_personRepo, 10).Wait();
        Utils.PopulateMemoriesAsync(_memoryRepo, _personRepo, 10).Wait();
        
        People = new ObservableCollection<Person>(_personRepo.GetAllAsync().Result);
        Memories = new ObservableCollection<Memory>(_memoryRepo.GetAllAsync().Result);
    }

    [RelayCommand]
    public void SlideNextCarousel(Carousel carousel)
    {
        carousel.Next();
    }
    
    [RelayCommand]
    public void SlidePreviousCarousel(Carousel carousel)
    {
        carousel.Previous();
    }
    
}