using MVVM_Personenverwaltung.Model;
using System.ComponentModel;
using System.Windows;

namespace MVVM_Personenverwaltung.ViewModel;

public class DetailViewModel : INotifyPropertyChanged
{
    //Command-Properties
    public CustomCommand OkCmd { get; set; }
    public CustomCommand AbbruchCmd { get; set; }

    //Property, welche die neue oder zu bearbeitende Person beinhaltet
    private Person neuePerson;
    public Person NeuePerson
    {
        get { return neuePerson; }
        set
        {
            neuePerson = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NeuePerson)));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public DetailViewModel()
    {
        NeuePerson = new Person();

        //OK-Command (Bestätigung)
        OkCmd = new CustomCommand
            (
                //CanExe: Kann immer ausgeführt werden. Eine Prüfung auf die Validierung der einzelnen Eingabefelder findet schon in der GUI (vgl. DetailView) statt.
                p => true,
                //Exe:
                p =>
                {
                    //Nachfrage auf Korrektheit der Daten per MessageBox
                    string ausgabe = NeuePerson.Vorname + " " + NeuePerson.Nachname + " (" + NeuePerson.Geschlecht + ")\n" + NeuePerson.Geburtsdatum.ToShortDateString() + "\n" + NeuePerson.Lieblingsfarbe.ToString();
                    ausgabe += NeuePerson.Verheiratet ? "\nIst Verheiratet" : "";
                    if (MessageBox.Show(ausgabe + "\nÜbernehmen?", "Neue Person", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        //Setzen des DialogResults des Views (welches per Parameter übergeben wurde) auf true, damit das ListView weiß, dass es weiter
                        //machen kann (d.h. die neuen Person einfügen bzw. austauschen)
                        (p as Window).DialogResult = true;
                        //Schließen des Views
                        (p as Window).Close();
                    }
                }
            );

        //Abbruch-Cmd
        AbbruchCmd = new CustomCommand
            (
                //CanExe: Kann immer ausgeführt werden
                p => true,
                //Exe: Schließen des Fensters
                p => (p as Window).Close()
            );
    }
}
