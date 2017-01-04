using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpotifyDotNetCore.Common
{
    public class SPArtist: SPItem
    {
        [JsonProperty("followers")]
        public dynamic Followers { get; set; }

        [JsonProperty("genres")]
        public IEnumerable<string> Genres { get; set; }

        [JsonProperty("images")]
        public IEnumerable<SPImage> Images { get; set; }

        [JsonProperty("popularity")]
        public uint? Popularity { get; set; }

        [JsonProperty("type")]
        public override SPItemType Type
        {
            get
            {
                return SPItemType.Artist;
            }
        }
    }
}