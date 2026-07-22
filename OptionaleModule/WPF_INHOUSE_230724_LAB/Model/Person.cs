using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Media;

namespace WPF_InhouseLab.Model
{
    public enum Gender { Männlich, Weiblich, Divers }

    public class Person : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string vorname;
        public string Vorname { get => vorname; set { vorname = value; OnPropertyChanged(); } }

        private string nachname;
        public string Nachname { get => nachname; set {nachname = value; OnPropertyChanged(); } }

        private DateTime geburtsdatum;
        public DateTime Geburtsdatum { get => geburtsdatum; set {geburtsdatum = value; OnPropertyChanged(); } }

        private bool verheiratet;
        public bool Verheiratet { get => verheiratet; set {verheiratet = value; OnPropertyChanged(); } }

        private Color lieblingsfarbe;
        public Color Lieblingsfarbe {get => lieblingsfarbe; set {lieblingsfarbe = value; OnPropertyChanged();}}

        private Gender geschlecht;
        public Gender Geschlecht { get => geschlecht; set { geschlecht = value; OnPropertyChanged(); } }

        private int kinder;
        public int Kinder { get => kinder; set { kinder = value; OnPropertyChanged(); } }

        public Person()
        {
            this.Geburtsdatum = DateTime.Now;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(propertyName)));


        public string Error => null;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {

                    case nameof(Vorname):
                        if (Vorname == null || Vorname.Length <= 0 || Vorname.Length > 50) return "Bitte geben Sie Ihren Vornamen ein.";
                        if (!Vorname.All(x => Char.IsLetter(x))) return "Der Vorname darf nur Buchstaben enthalten.";
                        if (Char.IsLower(Vorname.First())) return "Der Vorname muss mit einem Großbuchstaben beginnen";
                        break;

                    case nameof(Nachname):
                        if (Nachname == null || Nachname.Length <= 0 || Nachname.Length > 50) return "Bitte geben Sie Ihren Nachnamen ein.";
                        if (!Nachname.All(x => Char.IsLetter(x))) return "Der Nachname darf nur Buchstaben enthalten.";
                        if (Char.IsLower(Nachname.First())) return "Der Nachname muss mit einem Großbuchstaben beginnen";
                        break;

                    case nameof(Geburtsdatum):
                        if (Geburtsdatum > DateTime.Now) return "Das Geburtsdatum darf nicht in der Zukunft liegen.";
                        if (DateTime.Now.Year - Geburtsdatum.Year > 150) return "Das Geburtsdatum darf nicht mehr als 150 Jahre in der Vergangenheit liegen.";
                        break;

                    case nameof(Lieblingsfarbe):
                        if (Lieblingsfarbe.ToString().Equals("#00000000")) return "Wählen Sie Ihre Lieblingsfarbe aus.";
                        break;

                    case nameof(Kinder):
                        if (Kinder < 0) return "Dieser Wert muss mindestens '0' sein.";
                        break;
                }

                return String.Empty;
            }
        }
    }
}
