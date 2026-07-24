using System.Windows;

using ADaxer.MvvmNav.Abstractions.Navigation;

namespace DocuMan.UI.Wpf.Views;
/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window, IWpfShellView
{
    public MainWindow()
    {
        InitializeComponent();
    }
}