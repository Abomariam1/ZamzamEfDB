using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace TestUi.Converters
{
    public class ByteToImagConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] bytes && bytes != null && bytes.Length > 0)
            {
                MemoryStream memory = new(bytes!);
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = memory;
                image.EndInit();
                return image;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new ByteToImagConverter();
        }
    }
}
