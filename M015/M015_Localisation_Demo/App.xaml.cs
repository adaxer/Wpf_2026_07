using System.Windows;

namespace M15_Localisation;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    //Diese Methode wird zum Start der App ausgeführt
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
    }
}
