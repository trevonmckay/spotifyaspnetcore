using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SpotifyAspNetCore.Common
{
    public class SPAlbum: SPItem
    {
        [JsonProperty("album_type")]
        public SPAlbumType AlbumType { get; set; }

        [JsonProperty("artists")]
        public IEnumerable<SPArtist> Artists { get; set; } = Enumerable.Empty<SPArtist>();

        [JsonProperty("available_markets")]
        public IEnumerable<string> AvailableMarkets { get; set; } = Enumerable.Empty<string>();

        [JsonProperty("copyrights")]
        public IEnumerable<dynamic> Copyrights { get; set; }

        [JsonProperty("external_ids")]
        public Dictionary<string, string>  ExternalIds { get; set; }

        [JsonProperty("genres")]
        public IEnumerable<string> Genres { get; set; }

        [JsonProperty("images")]
        public IEnumerable<SPImage> Images { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("popularity")]
        public uint? Popularity { get; set; }

        [JsonProperty("release_date")]
        //[JsonConverter(typeof(SPDateTimeConverter))]
        public DateTime? ReleaseDate { get; set; }

        [JsonProperty("release_date_precision")]
        public string ReleaseDatePrecision { get; set; }

        [JsonProperty("tracks")]
        public IEnumerable<SPTrack> Tracks { get; set;  }

        [JsonProperty("type")]
        public override SPItemType Type
        {
            get
            {
                return SPItemType.Album;
            }
        }

        /*
        public SPImage Cover { get; set; }
        public SPImage SmallCover { get; set; }
        public SPImage LargeCover { get; set; }
        public Uri SpotifyURL { get; set; }
        public bool Loaded { get; set; }
        public bool Available { get; set; }*/
    }
}