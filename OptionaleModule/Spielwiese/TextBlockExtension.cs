using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Spielwiese
{
    internal class TextblockExtension : DependencyObject
    {
        public static bool GetAllowOnlyString(DependencyObject obj)
        {
            return (bool)obj.GetValue(AllowOnlyStringProperty);
        }
        public static void SetAllowOnlyString(DependencyObject obj, bool value)
        {
            obj.SetValue(AllowOnlyStringProperty, value);
        }
        // Using a DependencyProperty as the backing store for AllowOnlyString. This enables animation, styling, binding, etc...  
        public static readonly DependencyProperty AllowOnlyStringProperty =
        DependencyProperty.RegisterAttached("AllowOnlyString", typeof(bool), typeof(TextblockExtension), new PropertyMetadata(false, AllowOnlyString));
        private static void AllowOnlyString(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox)
            {
                TextBox txtObj = (TextBox)d;
                txtObj.TextChanged += (s, arg) =>
                {
                    TextBox txt = s as TextBox;
                    if (!Regex.IsMatch(txt.Text, "^[a-zA-Z]*$"))
                    {
                        txtObj.BorderBrush = Brushes.Red;
                        MessageBox.Show("Only letter allowed!");

                        txt.Text = txt.Text.Substring(0, txt.Text.Length - 1);
                    }
                };
            }
        }
    }
}
