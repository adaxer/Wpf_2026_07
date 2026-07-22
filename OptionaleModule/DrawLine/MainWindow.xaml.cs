using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace DrawLine;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public List<Tuple<int, int>> Coords { get; set; } = new List<Tuple<int, int>>()
    {
        new Tuple<int, int>(0,0),
        new Tuple<int, int>(100,100),
        new Tuple<int, int>(200,100),
        new Tuple<int, int>(400,200),
        new Tuple<int, int>(700,300),
    };

    List<Point> Points = new List<Point>();
    Storyboard sb;

    public MainWindow()
    {
        InitializeComponent();

        //for (int i = 0; i < Coords.Count-1; i++)
        //{
        //    Line line = new Line();
        //    line.Stroke = new SolidColorBrush(Colors.Black);
        //    line.StrokeThickness = 2;
        //    line.X1 = Coords[i].Item1;
        //    line.Y1 = Coords[i].Item2;
        //    line.X2 = Coords[i+1].Item1;
        //    line.Y2 = Coords[i+1].Item2;

        //    Cvs_Main.Children.Add(line);
        //}

        Points.Add(new Point(100, 200));
        Points.Add(new Point(500, 300));
        Points.Add(new Point(200, 200));
        Points.Add(new Point(400, 220));
        Points.Add(new Point(450, 200));
        Points.Add(new Point(500, 350));

        InitAnimation();

        sb.Begin(this);

    }

    public void InitAnimation()
    {
        sb = new Storyboard();

        for (int i = 0; i < Points.Count - 1; ++i)
        {
            //new line for current line segment
            var l = new Line();
            l.Stroke = Brushes.Black;
            l.StrokeThickness = 2;

            //data from list
            var startPoint = Points[i];
            var endPoint = Points[i + 1];

            //set startpoint = endpoint will result in the line not being drawn
            l.X1 = startPoint.X;
            l.Y1 = startPoint.Y;
            l.X2 = startPoint.X;
            l.Y2 = startPoint.Y;
            Cvs_Main.Children.Add(l);

            //Initialize the animations with duration of 1 second for each segment
            var daX = new DoubleAnimation(endPoint.X, new Duration(TimeSpan.FromMilliseconds(1000)));
            var daY = new DoubleAnimation(endPoint.Y, new Duration(TimeSpan.FromMilliseconds(1000)));
            //Define the begin time. This is sum of durations of earlier animations + 10 ms delay for each
            daX.BeginTime = TimeSpan.FromMilliseconds(i * 1010);
            daY.BeginTime = TimeSpan.FromMilliseconds(i * 1010);

            sb.Children.Add(daX);
            sb.Children.Add(daY);

            //Set the targets for the animations
            Storyboard.SetTarget(daX, l);
            Storyboard.SetTarget(daY, l);
            Storyboard.SetTargetProperty(daX, new PropertyPath(Line.X2Property));
            Storyboard.SetTargetProperty(daY, new PropertyPath(Line.Y2Property));
        }
    }
}
