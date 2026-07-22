using System.Windows;
using System.Windows.Controls;

namespace CustomLayouts;

/// <summary>
/// Spaltenbasiertes Layout-Panel, welches automatisch neue Splaten erzeugt,
/// wenn benötigt. Der User kann neue Spalten mit der ColumnBreakBefore-Property
/// erzwingen.
/// </summary>
public class ColumnedPanel : Panel
{

    #region Ctor
    static ColumnedPanel()
    {
        //Informierung des DP-Systems, dass diese DP Auswirkungen auf
        //Arrange (Position) und Measure (Ausdehnung) haben wird
        FrameworkPropertyMetadata metadata =
        new FrameworkPropertyMetadata();
        metadata.AffectsArrange = true;
        metadata.AffectsMeasure = true;
        ColumnBreakBeforeProperty =
            DependencyProperty.RegisterAttached(
            "ColumnBreakBefore",
            typeof(bool), typeof(ColumnedPanel),
            metadata);
    }
    #endregion

    #region DPs

    /// <summary>
    /// AttatchedProperty, welche zum Erzwingen von neuen Splaten verwendet werden kann
    /// </summary>
    public static DependencyProperty ColumnBreakBeforeProperty;

    public static void SetColumnBreakBefore(UIElement element,
        Boolean value)
    {
        element.SetValue(ColumnBreakBeforeProperty, value);
    }
    public static Boolean GetColumnBreakBefore(UIElement element)
    {
        return (bool)element.GetValue(ColumnBreakBeforeProperty);
    }
    #endregion

    #region Measure Override
    // From MSDN : When overridden in a derived class, measures the
    // size in layout required for child elements and determines a
    // size for the FrameworkElement-derived class

    //=> Methode, welche die Ausmaße des Panels selbst berechnet
    protected override Size MeasureOverride(Size constraint)
    {
        Size currentColumnSize = new Size();
        Size panelSize = new Size();

        foreach (UIElement element in base.InternalChildren)
        {
            //Erfragen der verlangten Größe eines Child-Objekts
            element.Measure(constraint);
            Size desiredSize = element.DesiredSize;

            // Erstelle eine neue Spalten (wenn erzwungen oder benötigte Höhe + schon benutzte Höhe > vorhandene Höhe)
            if (GetColumnBreakBefore(element) ||
                 currentColumnSize.Height + desiredSize.Height > constraint.Height)
            {
                panelSize.Height = Math.Max(currentColumnSize.Height, panelSize.Height);
                panelSize.Width += currentColumnSize.Width;
                currentColumnSize = desiredSize;

                // Whenn das Child größer ist, als eine Splate erlaubt, bekommt es seine ganz eigene Spalte
                if (desiredSize.Height > constraint.Height)
                {
                    panelSize.Height = Math.Max(desiredSize.Height, panelSize.Height);
                    panelSize.Width += desiredSize.Width;
                    currentColumnSize = new Size();
                }
            }
            // Ansonsten wird das Child zur schon bestehenden Splate hinzugefügt
            else
            {
                currentColumnSize.Height += desiredSize.Height;

                //Sichergehen, dass die Splate so breit sit, wie sein breitestes Element
                currentColumnSize.Width = Math.Max(desiredSize.Width, currentColumnSize.Width);
            }
        }

        //Rückgabe der insgesamt benötigten Größe, um alle Elemente beinhalten zu können.

        panelSize.Height = Math.Max(currentColumnSize.Height, panelSize.Height);
        panelSize.Width += currentColumnSize.Width;
        return panelSize;

    }
    #endregion

    #region Arrange Override
    //From MSDN : When overridden in a derived class, positions child
    //elements and determines a size for a FrameworkElement derived
    //class.

    //=> Methode zur Positionierung der Child-Elemente im Panel
    protected override Size ArrangeOverride(Size arrangeBounds)
    {
        int firstInLine = 0;

        Size currentColumnSize = new Size();

        double accumulatedWidth = 0;

        UIElementCollection elements = base.InternalChildren;
        for (int i = 0; i < elements.Count; i++)
        {

            Size desiredSize = elements[i].DesiredSize;

            //Erstellung einer neuen Spalte
            if (GetColumnBreakBefore(elements[i]) ||
                currentColumnSize.Height + desiredSize.Height > arrangeBounds.Height)
            {
                arrangeColumn(accumulatedWidth, currentColumnSize.Width, firstInLine, i, arrangeBounds);

                accumulatedWidth += currentColumnSize.Width;
                currentColumnSize = desiredSize;

                //Eigene Spalte für Objekte, die Höher sind als die Panel-Höhe
                if (desiredSize.Height > arrangeBounds.Height)
                {
                    arrangeColumn(accumulatedWidth, desiredSize.Width, i, ++i, arrangeBounds);
                    accumulatedWidth += desiredSize.Width;
                    currentColumnSize = new Size();
                }
                firstInLine = i;
            }
            else //ansosnten weiterführung der aktuellen Spalte
            {
                currentColumnSize.Height += desiredSize.Height;
                currentColumnSize.Width = Math.Max(desiredSize.Width, currentColumnSize.Width);
            }
        }

        if (firstInLine < elements.Count)
            arrangeColumn(accumulatedWidth, currentColumnSize.Width,  firstInLine, elements.Count, arrangeBounds);

        return arrangeBounds;
    }
    #endregion

    #region Private Methods
    /// <summary>
    /// Positionierung einer Spalte innerhalb des Panels
    /// </summary>
    private void arrangeColumn(double x, double columnWidth, int start, int end, Size arrangeBounds)
    {
        double y = 0;
        double totalChildHeight = 0;
        double widestChildWidth = 0;
        double xOffset = 0;

        UIElementCollection children = InternalChildren;
        UIElement child;

        for (int i = start; i < end; i++)
        {
            child = children[i];
            totalChildHeight += child.DesiredSize.Height;
            if (child.DesiredSize.Width > widestChildWidth)
                widestChildWidth = child.DesiredSize.Width;
        }

        y = ((arrangeBounds.Height - totalChildHeight) / 2);


        for (int i = start; i < end; i++)
        {
            child = children[i];
            if (child.DesiredSize.Width < widestChildWidth)
            {
                xOffset = ((widestChildWidth -
                    child.DesiredSize.Width) / 2);
            }

            child.Arrange(new Rect(x + xOffset, y,
                    child.DesiredSize.Width, columnWidth));
            y += child.DesiredSize.Height;
            xOffset = 0;
        }
    }
    #endregion

}