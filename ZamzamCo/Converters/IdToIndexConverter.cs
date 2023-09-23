using DesigenWPF;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ZamzamCo.Converters
{
    public class IdToIndexConverter : BaseValueConverter
    {
        private static readonly string[] _constr;
        //private static readonly DepartmentServices DepartmentService = new(new ZamzamDbContextFactory());
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if (value == null) return new();
            //var Dep = value as Department;

            return null;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var department = value as string;

            var dd = new object() { };
            return dd;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {

            var dt = (IValueConverter)new IdToIndexConverter();
            return dt;
        }
    }
}
