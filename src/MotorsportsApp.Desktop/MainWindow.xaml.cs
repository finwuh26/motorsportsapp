using System.Windows;
using MotorsportsApp.Core.ViewModels;

namespace MotorsportsApp.Desktop;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(MainViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        Loaded += async (s, e) => await viewModel.LoadDataCommand.ExecuteAsync(null);
    }
}