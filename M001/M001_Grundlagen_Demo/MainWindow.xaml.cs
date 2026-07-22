namespace M01_Grundlagen;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        //Dies verweist auf eine Methode in der (versteckten) automatisch generierten zweiten Klassen-Datei (*.g.i.cs),
        //welche für das Rendering des XAML-Codes verantwortlich ist. InitializeComponent() erstellt die
        //Steuerelement-Objekte und muss daher als erste Methode des Konstruktors bestehen bleiben
        InitializeComponent();
    }

    private void Btn_BeispielButton_Click(object sender, RoutedEventArgs e)
    {
        //Verändern des Inhalts des Buttons
        Btn_BeispielButton.Content = "Ich wurde angeklickt";
        //Ändern einer UI-Property über den Sender-Parameter (beinhaltet den Verursacher des Events)
        (sender as Button).Background = (SolidColorBrush)Application.Current.Resources["OkBrush"];
    }
}
