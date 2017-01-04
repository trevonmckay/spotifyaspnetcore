using System;
using Newtonsoft.Json;

namespace Spotify.NET.Common
{
    public class SPImage
    {
        [JsonProperty("height")]
        public uint? Height { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("width")]
        public uint? Width { get; set; }
    }
}