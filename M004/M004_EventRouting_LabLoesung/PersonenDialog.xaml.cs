using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lab04_Loesung;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class PersonenDialog : Window
{
    
    public PersonenDialog()
    {
        InitializeComponent();
    }

    #region Logging (Lab 04)

    //Property zum Speichern des Logs
    public StringBuilder Log { get; set; } = new StringBuilder("WindowLog:\n\n");

    //Abfangen von Tastendrücken
    private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        //Test, ob 'F1' gedrückt wurde
        if (e.Key == Key.F1)
            //Log-Anzeige
            MessageBox.Show(Log.ToString(), "Log", MessageBoxButton.OK);

        //Ansonsten: Eintrag ins Log
        else LogAction(sender, e);
    }

    //Logging einer Aktion
    private void LogAction(object sender, RoutedEventArgs e)
    {
        //Speichern des Namens des Quellelements
        string originalsource = e.OriginalSource is TextBox ? (e.OriginalSource as TextBox).Name : e.OriginalSource.ToString();

        //Testen auf Art des Events anhand der EventArgs und Eintrag ins Log
        if (e.GetType() == typeof(MouseButtonEventArgs))
            Log.Append($"{originalsource}: MouseButtonPressed: {(e as MouseButtonEventArgs).ChangedButton}");
        else if (e.GetType() == typeof(KeyEventArgs))
            Log.Append($"{originalsource}: KeyPressed: {(e as KeyEventArgs).Key}");
        else if (e.GetType() == typeof(TextChangedEventArgs))
            Log.Append($"{originalsource}: TextChanged: {(e.OriginalSource as TextBox).Text}");
        else
            Log.Append($"{originalsource}: Unknown Event");

        //Neue Zeile im Log
        Log.Append(Environment.NewLine);
    } 
    #endregion

}
