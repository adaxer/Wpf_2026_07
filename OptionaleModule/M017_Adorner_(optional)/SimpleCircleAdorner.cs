using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace M017_Adorner__optional_;

// Adorner-Klassen müssen von der abstrakten Klasse Adorner erben
public class SimpleCircleAdorner : Adorner
{
    // Die Basisklasse muss durch den Konstruktor aufgerufen werden
    public SimpleCircleAdorner(UIElement adornedElement)    //<= Das adornedElement ist das UIElement, welches ergänzt werden soll
      : base(adornedElement)
    {
    }

    // Standartmäßig wird das Renderverhalten des Adorners über die überschriebene OnRender-Methode implementiert.
    // Diese wird vom Layout als Teil des Renderings des Windows etc aufgerufen.
    protected override void OnRender(DrawingContext drawingContext)
    {
        //Erstellen des (unsichtbaren) umgebenden Rechtecks mittels der Größe des adornedElements
        Rect adornedElementRect = new Rect(this.AdornedElement.DesiredSize);

        //Voreinstellungen für den Pinsel der zu zeichnenden Kreise
        SolidColorBrush renderBrush = new SolidColorBrush(Colors.Green);
        renderBrush.Opacity = 0.2;
        Pen renderPen = new Pen(new SolidColorBrush(Colors.Navy), 1.5);
        double renderRadius = 5.0;

        // Zeichnen der Kreise in die Ecken des Rechtecks
        drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopLeft, renderRadius, renderRadius);
        drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.TopRight, renderRadius, renderRadius);
        drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomLeft, renderRadius, renderRadius);
        drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomRight, renderRadius, renderRadius);
    }
}