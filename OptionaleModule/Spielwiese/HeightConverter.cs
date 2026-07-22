using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Spielwiese
{
    internal class HeightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double gridrowheight = ((GridLength)values[0]).Value;
            double tb1height = (double)values[1];
            double tb2height = (double)values[2];

            return gridrowheight / 2 - tb1height - tb2height;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
