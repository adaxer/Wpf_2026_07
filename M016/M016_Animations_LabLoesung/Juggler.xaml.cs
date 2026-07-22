using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Lab16_Loesung;

/// <summary>
/// Interaction logic for UserControl1.xaml
/// </summary>
public partial class Juggler : UserControl
{
    public Juggler()
    {
        InitializeComponent();

        StartJuggle();
    }


    public void StartJuggle()
    {
        Storyboard movingPerson = (this.Resources["MovingPerson"] as Storyboard);

        movingPerson.RepeatBehavior = RepeatBehavior.Forever;

        movingPerson.Begin();
    }

}