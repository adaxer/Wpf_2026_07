using System.Windows;
using System.Windows.Input;

namespace Behaviors;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void MouseDragElementBehavior_DragBegun(object sender, MouseEventArgs e)
    {
        Lbl_Drag.Content = "DRAGGING";
    }

    private void MouseDragElementBehavior_DragFinished(object sender, MouseEventArgs e)
    {
        Lbl_Drag.Content = "DROPED";
    }
}
