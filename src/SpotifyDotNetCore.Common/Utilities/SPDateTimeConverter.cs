using Newtonsoft.Json.Converters;

namespace SpotifyDotNetCore.Common
{
    public class SPDateTimeConverter: IsoDateTimeConverter
    {
        public SPDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd";
        }
    }
}