using Newtonsoft.Json.Converters;

namespace Spotify.NET.Common
{
    public class SPDateTimeConverter: IsoDateTimeConverter
    {
        public SPDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd";
        }
    }
}