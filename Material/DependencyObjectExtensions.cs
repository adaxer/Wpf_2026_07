using System.Collections.Generic;
using System.Windows.Media;
using System.Windows;

namespace ExtensionMethods;
public static class DependencyObjectExtensions
{
    public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject depObj) where T : DependencyObject
    {
        if (depObj != null)
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                if (child is T dependencyObject)
                    yield return dependencyObject;

                foreach (var childOfChild in child.FindVisualChildren<T>())
                    yield return childOfChild;
            }
        }
    }

}
