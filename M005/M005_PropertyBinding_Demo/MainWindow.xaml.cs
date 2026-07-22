using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace M05_PropertyBinding;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        //Für die explizite Aktualisierung muss eine BindingExpression im CodeBehind erstellt werden und über die Methode UpdateSource() angefordert werden
        //Die BindingExpession wird per Übergabe der (statischen) DependencyProperty an die Methode GetBindingExpression() aus dem bindenen Objekt erhalten
        BindingExpression be = Tbx_Vier.GetBindingExpression(TextBox.TextProperty);
        be.UpdateSource();
    }
}
