using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpotifyDotNetCore.Common
{
    public class SPSearchResult
    {
        [JsonProperty("albums")]
        public SPPage Albums { get; set; }

        [JsonProperty("artists")]
        public SPPage Artists { get; set; }

        [JsonProperty("playlists")]
        public SPPage Playlists { get; set; }

        [JsonProperty("tracks")]
        public SPPage Tracks { get; set; }

        public IEnumerable<SPItem> GetAllResults()
        {
            var collection = new List<SPItem>();

            if (this.Albums != null)
                collection.AddRange(this.Albums.Items);

            if (this.Artists != null)
                collection.AddRange(this.Artists.Items);

            if (this.Playlists != null)
                collection.AddRange(this.Playlists.Items);

            if (this.Tracks != null)
                collection.AddRange(this.Tracks.Items);

            return collection;
        }
    }
}
