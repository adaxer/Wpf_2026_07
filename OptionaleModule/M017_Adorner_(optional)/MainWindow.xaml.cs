using System.Windows;
using System.Windows.Documents;

namespace M017_Adorner__optional_;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        Tbx_Show.Loaded += (s, e) =>
        {
            //Diese Methode durchläuft den VisualTree ausgehend von dem eingebenen UIElement nach oben und gibt das
            //erste AdornerLayer zurück, welches es findet. Die Elemente müssen dafür schon vorhanden sein. Hier
            //bekommen wir das Layer von der Textbox zurück.
            AdornerLayer adLayer = AdornerLayer.GetAdornerLayer(Tbx_Show);

            //Hinzufgen des Adorners zum Layer
            adLayer.Add(new SimpleCircleAdorner(Tbx_Show));

        };
    }
}
