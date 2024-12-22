using Avalonia;
using Avalonia.Controls;

namespace Meminisse.Views;
public partial class MainWindow : Window
{
    private readonly Control _content;
    private readonly Border _sidebar;
    public MainWindow()
    {
        InitializeComponent();
        _content = Content;
        _sidebar = Sidebar;
    }

    private void Window_SizeChanged(object? sender, Avalonia.Controls.SizeChangedEventArgs e)
    {
        // Check window width and switch between layouts
        // SIDEBAR LAYOUT
        if (Bounds.Width >= 800)
        {
            // Wide layout: Sidebar
            MainGrid.RowDefinitions.Clear();
            MainGrid.ColumnDefinitions.Clear();
            MainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
            MainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200) });
            MainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });

            Grid.SetColumn(_sidebar, 0);
            _sidebar.BorderThickness = new Thickness(0, 0, 2, 0);
            MainGrid.Children.Clear();
            MainGrid.Children.Add(_sidebar);

            // Add Main View
            Grid.SetColumn(_content, 1);
            MainGrid.Children.Add(_content);
        }
        // TOPBAR LAYOUT
        else
        {
            // Narrow layout: Top Bar
            MainGrid.RowDefinitions.Clear();
            MainGrid.ColumnDefinitions.Clear();
            MainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50) });
            MainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });

            Grid.SetRow(_sidebar, 0);
            _sidebar.BorderThickness = new Thickness(0, 0, 0, 2);
            MainGrid.Children.Clear();
            MainGrid.Children.Add(_sidebar);

            // Add Main View
            Grid.SetRow(_content, 1);
            MainGrid.Children.Add(_content);
        }

    }
}