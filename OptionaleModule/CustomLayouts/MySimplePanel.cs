using System.Windows;
using System.Windows.Controls;

namespace CustomLayouts;

public class MySimplePanel : Panel
{
    // Panelgröße wird auf die Größe des größten Elements gesetzt
    protected override Size MeasureOverride(Size availableSize)
    {
        Size maxSize = availableSize;

        foreach (UIElement child in base.InternalChildren)
        {
            child.Measure(availableSize);
            maxSize.Height = Math.Max(child.DesiredSize.Height, maxSize.Height);
            maxSize.Width = Math.Max(child.DesiredSize.Width, maxSize.Width);
        }

        return maxSize;
    }

    Random random = new Random();

    //Positionierung der Child-Elemente
    protected override Size ArrangeOverride(Size finalSize)
    {
        foreach (UIElement child in base.InternalChildren)
        {
            double muliplier = random.NextDouble() + 0.5;

            Size newSize = new Size(finalSize.Width * muliplier, finalSize.Height * muliplier);
            Point newPosition = new Point(random.Next(0, (int)finalSize.Height / 2), random.Next(0, (int)finalSize.Width / 2));

            child.Arrange(new Rect(newSize));
        }

        //for (int i = 0; i < base.InternalChildren.Count; i++)
        //{
        //    base.InternalChildren[i].Arrange(new Rect(new Point(i * 50, i * 50), base.InternalChildren[i].DesiredSize));
        //}

        return finalSize;
    }
}
