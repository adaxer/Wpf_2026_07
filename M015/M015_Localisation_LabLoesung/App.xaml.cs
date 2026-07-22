using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Markup;

namespace Lab1314_Lösung;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);  
        
        if (File.Exists("settings.txt"))
        {
            string text = File.ReadAllText("settings.txt");

            if (text == "language=en-US")
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");

            FrameworkElement.LanguageProperty.OverrideMetadata(
            typeof(FrameworkElement),
            new FrameworkPropertyMetadata(
                XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.Name)));
        }
    }
}
