using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WPF_InhouseLab.Model
{
    public class DbController
    {
        private Random random = new Random();
        private List<Person> peopleInDb = new List<Person>()
        {
            new Person(){Vorname="Rainer", Nachname="Zufall", Geburtsdatum=new DateTime(1987, 5, 13), Verheiratet=true, Lieblingsfarbe=Colors.DarkSeaGreen, Geschlecht=Gender.Männlich, Kinder=2},
            new Person(){Vorname="Anna", Nachname="Nass", Geburtsdatum=new DateTime(1974, 11, 29), Verheiratet=false, Lieblingsfarbe=Colors.LightBlue, Geschlecht=Gender.Weiblich, Kinder=0}
        };

        public List<Person> GetPeople()
        {
            Thread.Sleep(random.Next(1, 5) * 1000);
            return peopleInDb;
        }

        public bool AddPerson(Person person)
        {
            Thread.Sleep(random.Next(1, 5) * 1000);
            peopleInDb.Add(person);
            return true;
        }

        public bool RemovePerson(Person person)
        {
            Thread.Sleep(random.Next(1, 5) * 1000);
            peopleInDb.Remove(person);
            return true;
        }
    }
}
