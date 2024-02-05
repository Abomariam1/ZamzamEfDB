namespace ZamzamUiCompact.Helpers;

public class BytesToImageConverter : MarkupExtension, IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is byte[] bytes && bytes != null && bytes.Length > 0 && bytes[0] > 0)
        {
            MemoryStream memory = new(bytes!);
            var image = new BitmapImage();
            try
            {
                image.BeginInit();
                image.StreamSource = memory;
                image.EndInit();
            }
            catch (Exception e)
            {
                var data = e.Data;
                return null;
            }
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
        return new BytesToImageConverter();
    }
}
