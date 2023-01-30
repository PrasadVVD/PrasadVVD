using System;
using System.Globalization;
using Xamarin.Forms;

namespace ArtosApp.Converters
{
    public class NotConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool bValue) && !bValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool bValue) && !bValue;
        }

    }
}
