using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using Microsoft.Xaml.Behaviors;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Spielwiese
{
    public static class DigitsOnlyBehavior
    {
        public static bool GetIsDigitOnly(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDigitOnlyProperty);
        }

        public static void SetIsDigitOnly(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDigitOnlyProperty, value);
        }

        public static readonly DependencyProperty IsDigitOnlyProperty =
          DependencyProperty.RegisterAttached("IsDigitOnly",
          typeof(bool), typeof(DigitsOnlyBehavior),
          new PropertyMetadata(false, OnIsDigitOnlyChanged));

        private static void OnIsDigitOnlyChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // ignoring error checking
            var textBox = (TextBox)sender;
            var isDigitOnly = (bool)(e.NewValue);

            if (isDigitOnly)
                textBox.PreviewTextInput += BlockNonDigitCharacters;
            else
                textBox.PreviewTextInput -= BlockNonDigitCharacters;
        }

        private static void BlockNonDigitCharacters(object sender, TextCompositionEventArgs e)
        {
            e.Handled = e.Text.Any(ch => !Char.IsDigit(ch));
        }
    }

    public class RotateOnClickBehavior : Behavior<UIElement>
    {
        private readonly RotateTransform rotateTransform = new RotateTransform();
        private DoubleAnimation rotateAnimation = new DoubleAnimation(360, new Duration(TimeSpan.FromMilliseconds(2000)));
        private Storyboard sb = new Storyboard();

        protected override void OnAttached()
        {
            Window parent = Application.Current.MainWindow;
            AssociatedObject.RenderTransform = rotateTransform;

            AssociatedObject.RenderTransformOrigin = new Point(0.5, 0.5);

            sb.Children.Add(rotateAnimation);
            Storyboard.SetTarget(rotateAnimation, AssociatedObject);
            Storyboard.SetTargetProperty(rotateAnimation, new PropertyPath("(UIElement.RenderTransform).(RotateTransform.Angle)"));

            sb.Completed += (s, e) => sb.Stop();

            AssociatedObject.MouseLeftButtonUp += (sender, e) =>
            {
                (AssociatedObject.RenderTransform as RotateTransform).Angle = 0;

                sb.Begin();
            };
        }
    }
}
