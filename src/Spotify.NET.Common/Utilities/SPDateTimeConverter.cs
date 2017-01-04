using Newtonsoft.Json.Converters;

namespace Spotify.NET.Common
{
    internal class SPDateTimeConverter: IsoDateTimeConverter
    {
        public SPDateTimeConverter()
        {
            base.DateTimeFormat = "yyyy-MM-dd";
        }
    }
}