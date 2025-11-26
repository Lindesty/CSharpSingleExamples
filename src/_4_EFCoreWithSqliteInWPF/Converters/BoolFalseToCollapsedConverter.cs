using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace _4_EFCoreWithSqliteInWPF.Converters;

public class BoolFalseToCollapsedConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is true ? Visibility.Visible : Visibility.Collapsed;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is Visibility.Visible;
    }
}