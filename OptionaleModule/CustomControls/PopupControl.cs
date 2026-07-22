using System.Windows;
using System.Windows.Controls;

namespace CustomControls;

//Die TemplatePart-Attribute definieren, welche Controls Teil des ControlTemplates sein müssen, dass diesem CustomControl zugeordnet wird
[TemplatePart(Name ="Content", Type =typeof(ContentPresenter))]
[TemplatePart(Name ="PopupContent", Type =typeof(ContentPresenter))]
public class PopupControl : ContentControl
{
    static PopupControl()
    {
        //Überschreiben des 'Standart-Styles' des Controls mit dem in Generic.xaml definierten Style durch Übergabe eines Type-Objekts der aktuellen Klasse
        DefaultStyleKeyProperty.OverrideMetadata(typeof(PopupControl), new FrameworkPropertyMetadata(typeof(PopupControl)));
    }

    //DP für Inhalt in Popup
    public object PopupContent
    {
        get { return (object)GetValue(PopupContentProperty); }
        set { SetValue(PopupContentProperty, value); }
    }
    public static readonly DependencyProperty PopupContentProperty =
        DependencyProperty.Register("PopupContent", typeof(object), typeof(PopupControl), new PropertyMetadata(null));


    public PopupControl()
    {
    }
}
