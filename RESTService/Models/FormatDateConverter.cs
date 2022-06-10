using Newtonsoft.Json.Converters;

namespace RESTService.Models
{
    public class FormatDateConverter : IsoDateTimeConverter
    {
        public FormatDateConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}
