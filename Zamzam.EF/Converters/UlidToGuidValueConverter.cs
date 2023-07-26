using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Zamzam.EF.Converters
{
    public class UlidToGuidValueConverter
    {
        public static ValueConverter ulidconverter => new ValueConverter<Ulid, string>
             (
                 v => v.ToString(),
                 v => Ulid.Parse(v.ToString())
             );
    }
}
