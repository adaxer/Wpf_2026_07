using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace M15_Localisation;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        this.DataContext = this;
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        //Testen der aktuellen Sprache
        if (Thread.CurrentThread.CurrentUICulture.Name == "en-US")
            //Ändern der Sprache
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("de-DE");
        else
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");

        //Neu-Öffnen des aktuellen Fensters (mit neuer Sprache)
        new MainWindow().Show();

        //Schließen des alten Fensters
        this.Close();
    }


    public TestEnum SelectedEnumValue { get; set; }

    private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        Tbl_SelectedEnum.Text = SelectedEnumValue.ToString();
    }
}

public enum TestEnum { TestEnum_1, TestEnum_2 , TestEnum_3 , TestEnum_4 }

//Converter zum Umwandeln des Enum-Werts in ComboBoxEintrag-String
public sealed class EnumToStringConverter : IValueConverter
{
    //Enum -> lokalisierter ComboBoxEintrag
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
            return null;

        //Zugriff auf ResX
        return Loc.Strings.ResourceManager.GetString(value.ToString());
    }

    //lokalisierter ComboBoxEintrag -> Enum
    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        string str = (string)value;

        foreach (object enumValue in Enum.GetValues(targetType))
        {
            if (str == Loc.Strings.ResourceManager.GetString(enumValue.ToString()))
                return enumValue; 
        }

        throw new ArgumentException(str);
    }
}

//Spezielles Markup-Extension-Objekt
public sealed class EnumerateExtension : MarkupExtension
{
    //Type = Enum-Typ
    public Type Type { get; set; }
    //Der Übergabe-Parameter wird in XAML direkt hinter den Aufruf der Markupextension gesetzt (vgl. XAML)
    public EnumerateExtension(Type type)
    {
        this.Type = type;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        //Umwandlung der Enums in lokalisierte Strings und Rückgabe dieser an den Aufrufer (hier wird eine ItemSource-Property erwartet)
        string[] names = Enum.GetNames(Type);
        string[] values = new string[names.Length];

        for (int i = 0; i < names.Length; i++)
            values[i] = Loc.Strings.ResourceManager.GetString(names[i]);

        return values;
    }
}
