using Microsoft.Xaml.Behaviors;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace WPF_InhouseLab
{
    internal class DisableIfFailedValidationBehavior : Behavior<System.Windows.Controls.Button>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            MultiBinding mb = CreateMultiBinding();

            AssociatedObject.SetBinding(Button.IsEnabledProperty, mb);
        }

        private MultiBinding CreateMultiBinding()
        {
            MultiBinding multiBinding = new MultiBinding();

            FrameworkElement fe = AssociatedObject;
            do
            {
                if (fe is Window) break;

                fe = fe.Parent as FrameworkElement;

                if (fe == null) throw new NullReferenceException("FE is null");
            } while (true);

            Panel container = (fe as Window).Content as Panel;

            AddBindings(container, multiBinding);

            multiBinding.Converter = new ValidationConverter();

            return multiBinding;
        }

        private void AddBindings(Panel container, MultiBinding mb)
        {
            foreach (var child in container.Children)
            {
                if(child is Panel) AddBindings(child as Panel, mb);

                else if (child is FrameworkElement)
                {
                    FrameworkElement fe = child as FrameworkElement;
                    ControlTemplate ct = Validation.GetErrorTemplate(fe);
                    if (ct != null && ct.HasContent == true)
                        mb.Bindings.Add(new Binding() { ElementName = fe.Name, Path = new PropertyPath(Validation.HasErrorProperty) });

                }
            }
        }

        class ValidationConverter : IMultiValueConverter
        {
            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
                =>  !values.Any(value => (bool)value == true);
            

            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
                => throw new NotImplementedException();
        }
    }
}
