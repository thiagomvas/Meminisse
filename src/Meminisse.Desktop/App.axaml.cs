using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using Meminisse.Application;
using Meminisse.Application.Repositories;
using Meminisse.Core.Abstractions;
using Meminisse.Core.Entities;
using Meminisse.Desktop.ViewModels;
using Meminisse.Desktop.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Meminisse.Desktop;

public partial class App : Avalonia.Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        
        ServiceCollection services = new();
        services.AddDbContext<MeminisseDbContext>(o => o.UseInMemoryDatabase("Meminisse"));
        services.AddScoped<IRepository<Memory>, MemoryRepository>();
        services.AddScoped<IRepository<MemoryItem>, MemoryItemRepository>();
        services.AddScoped<IRepository<MemoryTag>, MemoryTagRepository>();
        services.AddScoped<IRepository<Tag>, TagRepository>();
        services.AddScoped<IRepository<Person>, PersonRepository>();
        services.AddScoped<IRepository<PersonTag>, PersonTagRepository>();
        services.AddScoped<IRepository<MemoryPerson>, MemoryPersonRepository>();

        services.AddScoped<MainWindowViewModel>();
        services.AddScoped<HomeViewModel>();
        services.AddScoped<SettingsViewModel>();

        var provider = services.BuildServiceProvider();
        
        
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow
            {
                DataContext = provider.GetRequiredService<MainWindowViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }
}