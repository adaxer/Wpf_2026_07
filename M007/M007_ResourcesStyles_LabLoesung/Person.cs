using System.ComponentModel;
using System.Windows.Media;

namespace Lab07_Loesung;

public enum Gender { Männlich, Weiblich, Divers }

public class Person : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler PropertyChanged;

    private string vorname;
    public string Vorname { get => vorname; set { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Vorname))); vorname = value; } }

    private string nachname;
    public string Nachname { get => nachname; set { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Nachname))); nachname = value; } }

    private DateTime geburtsdatum;
    public DateTime Geburtsdatum { get => geburtsdatum; set { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Geburtsdatum))); geburtsdatum = value; } }

    private bool verheiratet;
    public bool Verheiratet { get => verheiratet; set { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Verheiratet))); verheiratet = value; } }

    private Color lieblingsfarbe;
    public Color Lieblingsfarbe { get => lieblingsfarbe; set { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Lieblingsfarbe))); lieblingsfarbe = value; } }

    private Gender geschlecht;
    public Gender Geschlecht { get => geschlecht; set { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Geschlecht))); geschlecht = value; } }

    public Person()
    {
        this.Vorname = String.Empty;
        this.Nachname = String.Empty;
        this.Geburtsdatum = DateTime.Now;
    }
}
