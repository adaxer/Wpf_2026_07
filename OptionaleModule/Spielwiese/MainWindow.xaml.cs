using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Spielwiese
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            TheNumber = 5.05F;

            NumberFormat = "F8";

            this.DataContext = this;


            //Tbx_Test.GetBindingExpression(TextBox.TextProperty).ParentBinding.StringFormat = "G";

            //Tbx_Test.SetBinding(TextBox.TextProperty, new Binding("TheNumber") { StringFormat = "F8" });

        }

        public float TheNumber { get; set; }
        public string NumberFormat { get; set; }

        private void Btn_Change_Click(object sender, RoutedEventArgs e)
        {
            //Tbx_Test.SetBinding(TextBox.TextProperty, new Binding("TheNumber") { StringFormat = "G" });
        }
    }
}
