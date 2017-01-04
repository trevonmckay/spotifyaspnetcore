using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SpotifyAspNetCore.Common
{
    public class SPItemConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(SPItem));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            IEnumerable<SPItem> spItems = null;
            if (reader.Path == "albums.items")
            {
                spItems = serializer.Deserialize<IEnumerable<SPAlbum>>(reader);
            }
            else if (reader.Path == "artists.items")
            {
                spItems = serializer.Deserialize<IEnumerable<SPArtist>>(reader);
            }
            else if (reader.Path == "tracks.items")
            {
                spItems = serializer.Deserialize<IEnumerable<SPTrack>>(reader);
            }

            return spItems;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

    }
    /*
    public interface ISPItem
    {
        [JsonProperty("external_urls")]
        IEnumerable<Dictionary<string, string>> ExternalUrls { get; set; }

        [JsonProperty("href")]
        string Href { get; set; }

        [JsonProperty("id")]
        string Id { get; set; }

        [JsonProperty("type")]
        SPItemType Type { get; }

        [JsonProperty("uri")]
        Uri Uri { get; set; }
    }
    */
    public abstract class SPItem
    {
        [JsonProperty("external_urls")]
        public Dictionary<string, string> ExternalUrls { get; set; }

        [JsonProperty("href")]
        public string Href { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public abstract SPItemType Type { get; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        public static SPItemType ConvertStringToEnum(string input)
        {
            switch (input.ToLower().Trim())
            {
                case "album":
                    return SPItemType.Album;
                case "artist":
                    return SPItemType.Artist;
                case "playlist":
                    return SPItemType.Playlist;
                case "track":
                    return SPItemType.Track;
                default:
                    throw new ArgumentOutOfRangeException(nameof(input), input, $"{input} is not a valid SPItemType.");
            }
        }
    }
}
