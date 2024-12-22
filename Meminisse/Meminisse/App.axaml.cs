using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Meminisse.Application;
using Meminisse.ViewModels;
using Meminisse.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Meminisse;
public partial class App : Avalonia.Application
{

    public static IServiceProvider ServiceProvider { get; private set; }
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {

        var collection = new ServiceCollection();
        collection.AddScoped<MainViewModel>();
        collection.AddScoped<HomeViewModel>();
        collection.AddScoped<MainWindowViewModel>();
        collection.AddScoped<MeminisseDbContext>();

        ServiceProvider = collection.BuildServiceProvider();

        var dbContext = ServiceProvider.GetRequiredService<MeminisseDbContext>();
        await dbContext.Database.EnsureCreatedAsync();
        await dbContext.Database.MigrateAsync();

        var vm = ServiceProvider.GetRequiredService<MainWindowViewModel>();
        vm.NavigateTo(ServiceProvider.GetRequiredService<HomeViewModel>());

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = vm
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = vm
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}