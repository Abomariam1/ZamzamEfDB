using DesigenWPF;
using System;
using System.Globalization;
using System.Windows.Data;
using Zamzam.Core;
using Zamzam.EF;
using Zamzam.EF.Services;

namespace ZamzamCo.Converters
{
    public class IdToIndexConverter : BaseValueConverter
    {
        private static readonly DepartmentServices DepartmentService = new(new ZamzamDbContextFactory());
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return new();
            var Dep = value as Department;

            return Dep.DepName;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var department = value as string;

            var dd = DepartmentService.GetDepartmentByName(department);
            return dd;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {

            var dt = (IValueConverter)new IdToIndexConverter();
            return dt;
        }
    }
}
