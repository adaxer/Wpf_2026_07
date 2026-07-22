using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace M13_Commands;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        //Initialisierung der Commands
        CloseCmd = new CloseCommand();
        OeffnenCmd = new CustomCommand
            (
                //Übergabe der Execute()-Logik
                p => (new MainWindow()).Show(),
                //Übergabe der CanExecute()-Logik
                p => (p as string).Length >= 1
            );

        //Setzen des DataContext
        this.DataContext = this;
    }

    //Commandproperties 
    public CloseCommand CloseCmd { get; set; }
    public CustomCommand OeffnenCmd { get; set; }

    public static RoutedUICommand MyCmd { get; set; } = new RoutedUICommand("Mein Command", "Mein Command", typeof(MainWindow));



    //Logik des Delete-Commands
    private void Delete_CanExecute(object sender, CanExecuteRoutedEventArgs e)
    {
        e.CanExecute = !string.IsNullOrEmpty((e.OriginalSource as TextBox).Text);
    }

    private void Delete_Executed(object sender, ExecutedRoutedEventArgs e)
    {
        (e.OriginalSource as TextBox).Text = "";
    }
}
