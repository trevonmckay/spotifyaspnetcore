using Newtonsoft.Json.Converters;

namespace SpotifyAspNetCore.Common
{
    public class SPDateTimeConverter: IsoDateTimeConverter
    {
        public SPDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd";
        }
    }
}