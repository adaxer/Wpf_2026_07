using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_InhouseLab.Model;

namespace WPF_InhouseLab.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Wnd_PersonenDialog : Window
    {
        
        public Wnd_PersonenDialog()
        {
            InitializeComponent();
        }

        private void Btn_Ok_Click(object sender, RoutedEventArgs e)
        {
            Person neuePerson = this.DataContext as Person;

            string ausgabe = neuePerson.Vorname + " " + neuePerson.Nachname + " (" + neuePerson.Geschlecht + ")\n" + neuePerson.Geburtsdatum.ToShortDateString() + "\n" + neuePerson.Lieblingsfarbe.ToString();
            if (neuePerson.Verheiratet) ausgabe = ausgabe + "\nIst verheiratet";
            if (neuePerson.Kinder > 0) ausgabe = ausgabe + $"\nHat {neuePerson.Kinder} {(neuePerson.Kinder == 1 ? "Kind" : "Kinder")}";
            if (MessageBox.Show(ausgabe + "\nAbspeichern?", neuePerson.Vorname + " " + neuePerson.Nachname, MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
            {
                this.DialogResult = true;
                this.Close();
            }
        }

        private void Btn_Abbruch_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Mit_Deutsch_Click(object sender, RoutedEventArgs e) => ÄndereSprache("de-DE");

        private void Mit_Englisch_Click(object sender, RoutedEventArgs e) => ÄndereSprache("en-US");

        private void ÄndereSprache(string cultureCode)
        {
            if (Thread.CurrentThread.CurrentUICulture.Name != cultureCode)
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(cultureCode);

                this.GetBindingExpression(Window.TitleProperty).UpdateTarget();
                Mit_Sprache.GetBindingExpression(MenuItem.HeaderProperty).UpdateTarget();
                Mit_Deutsch.GetBindingExpression(MenuItem.HeaderProperty).UpdateTarget();
                Mit_Englisch.GetBindingExpression(MenuItem.HeaderProperty).UpdateTarget();

                

                foreach (var item in Grd_Main.Children)
                    (item as TextBlock)?.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();

                foreach (var item in Wpl_Buttons.Children)
                    (item as Button)?.GetBindingExpression(Button.ContentProperty).UpdateTarget();

                foreach (var item in Spl_Geschlecht.Children)
                    if (item is RadioButton) (item as RadioButton).GetBindingExpression(RadioButton.ContentProperty).UpdateTarget();

                DataTemplate dt = Cbb_Lieblingsfarbe.ItemTemplate;
                int index = Cbb_Lieblingsfarbe.SelectedIndex;

                Cbb_Lieblingsfarbe.SelectedIndex = 0;
                Cbb_Lieblingsfarbe.ItemTemplate = null;

                Cbb_Lieblingsfarbe.ItemTemplate = dt;
                Cbb_Lieblingsfarbe.SelectedIndex = index;

                //Dpr_Geburtsdatum.Set
                

                //Window newWnd = new PersonenDialog() { DataContext = this.DataContext };
                //newWnd.Show();

                    //this.Close();
            }
        }
    }
}
