using System.Windows;
using System.Windows.Media;

namespace M07_Resources_Styles;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        //Manipulation einer Ressource über deren Container und Key (Objekte, welche dynamisch angebunden sind, übernehmen sofort die Veränderung)
        Spl_Main.Resources["Scb_LightGreen"] = new SolidColorBrush(Colors.LightBlue);
    }
}
