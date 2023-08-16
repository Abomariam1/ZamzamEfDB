using DesigenWPF;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ZamzamCo.Converters
{
    public class DateTimeToDateOnlyConverter : BaseValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var date = (DateOnly)value;
            if (value is not DateOnly)
            {
                return DateTime.Now;
            }
            return date.ToDateTime(TimeOnly.MinValue);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var dt = (DateTime)value;
            return DateOnly.FromDateTime(dt);
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {

            var dt = (IValueConverter)new DateTimeToDateOnlyConverter();
            return dt;
        }
    }
}
