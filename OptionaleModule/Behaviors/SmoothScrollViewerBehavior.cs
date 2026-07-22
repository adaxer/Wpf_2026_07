using Microsoft.Xaml.Behaviors;
using System.Reflection;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace Behaviors;

public class SmoothScrollViewerBehavior : Behavior<System.Windows.Controls.ScrollViewer>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.Loaded += ScrollViewerLoaded;
    }

    private void ScrollViewerLoaded(object sender, RoutedEventArgs e)
    {
        PropertyInfo? property = AssociatedObject.GetType().GetProperty("ScrollInfo", BindingFlags.NonPublic | BindingFlags.Instance);
        property.SetValue(AssociatedObject, new ScrollInfoAdapter((IScrollInfo)property.GetValue(AssociatedObject)));
    }
}
