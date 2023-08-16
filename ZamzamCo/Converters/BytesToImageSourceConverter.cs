using DesigenWPF;
using System;
using System.Globalization;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ZamzamCo.Converters
{
    public class BytesToImageSourceConverter : BaseValueConverter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var source = value as byte[];
            ImageSource imgsource;
            if (source == null)
            {
                return null;
            }
            MemoryStream memory = new(source);
            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = memory;
            image.EndInit();
            imgsource = image as ImageSource;
            return imgsource;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var x = value;
            return x;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return new BytesToImageSourceConverter();
        }
    }
}
