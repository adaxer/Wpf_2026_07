using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WPF_InhouseLab.Loc
{
    public class ColorLocalisationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string localizedColor = value.ToString();
            Color[] usedColors = parameter as Color[];

            for (int i = 0; i < usedColors.Length; i++)
            {
                if (usedColors[i].Equals(value))
                    localizedColor = PersonenDialog_Strings.ResourceManager.GetString($"usedColors_{i}");
            }

            return localizedColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
