using System.Globalization;
using System.Windows.Data;

namespace Lab07_Loesung;

public class GenderToBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return value.Equals(parameter);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? parameter : Binding.DoNothing;

        if ((bool)value)
            return parameter;
        else
            return Binding.DoNothing;
    }
}
