using System.Windows;

using DocuMan.UI.Common.ViewModels;
using DocuMan.UI.Wpf.Views;

namespace DocuMan.UI.Wpf;
/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        MainWindow = new MainWindow();
        MainWindow.DataContext = new MainViewModel();
        MainWindow.Show();
        base.OnStartup(e);
    }
}

