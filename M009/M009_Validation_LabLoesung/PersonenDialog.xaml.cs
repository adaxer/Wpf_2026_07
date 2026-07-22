using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Lab09_Loesung;

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

    public StringBuilder Log { get; set; } = new StringBuilder("WindowLog:\n\n");

    private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.F1)
            MessageBox.Show(Log.ToString(), "Log", MessageBoxButton.OK);
        else LogAction(sender, e);
    }

    private void LogAction(object sender, RoutedEventArgs e)
    {
        string originalsource = e.OriginalSource is TextBox ? (e.OriginalSource as TextBox).Name : e.OriginalSource.ToString();

        if (e.GetType() == typeof(MouseButtonEventArgs))
            Log.Append($"{originalsource}: MouseButtonPressed: {(e as MouseButtonEventArgs).ChangedButton}");
        else if (e.GetType() == typeof(KeyEventArgs))
            Log.Append($"{originalsource}: KeyPressed: {(e as KeyEventArgs).Key}");
        else if (e.GetType() == typeof(TextChangedEventArgs))
            Log.Append($"{originalsource}: TextChanged: {(e.OriginalSource as TextBox).Text}");
        else
            Log.Append($"{originalsource}: Unknown Event");

        Log.Append(Environment.NewLine);
    }
    #endregion

    private void Btn_Ok_Click(object sender, RoutedEventArgs e)
    {
        Person neuePerson = this.DataContext as Person;

        string ausgabe = neuePerson.Vorname + " " + neuePerson.Nachname + " (" + neuePerson.Geschlecht + ")\n" + neuePerson.Geburtsdatum.ToShortDateString() + "\n" + neuePerson.Lieblingsfarbe.ToString();
        if (neuePerson.Verheiratet) ausgabe = ausgabe + "\nIst verheiratet";
        if (MessageBox.Show(ausgabe + "\nAbspeichern?", neuePerson.Vorname + " " + neuePerson.Nachname, MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
        {
            this.Close();
        }
    }

    private void Btn_Abbruch_Click(object sender, RoutedEventArgs e)
    {

    }
}
