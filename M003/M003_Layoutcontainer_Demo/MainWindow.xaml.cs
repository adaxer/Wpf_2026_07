using System.Windows;
using System.Windows.Controls;

namespace M03_Container;

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
        //Codeseitiger Schreib-Zugriff auf eine Attached-Property des Canvas
        Canvas.SetLeft(Rct_Blue, Canvas.GetLeft(Rct_Blue) + 10);
        Rct_Blue.SetValue(Canvas.BottomProperty, Canvas.GetBottom(Rct_Blue) + 2);
    }
}