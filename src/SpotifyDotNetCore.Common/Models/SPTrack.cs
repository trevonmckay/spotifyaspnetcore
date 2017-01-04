using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SpotifyAspNetCore.Common
{
    public class SPTrack : SPItem
    {
        private class SPTrackDurationConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return (objectType == typeof(TimeSpan));
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                int milliseconds = 0;
                int.TryParse(reader.Value.ToString(), out milliseconds);

                return new TimeSpan(0, 0, 0, 0, milliseconds);
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var timeSpan = (TimeSpan)value;
                writer.WriteStartObject();
                writer.WritePropertyName("duration");
                serializer.Serialize(writer, timeSpan.TotalMilliseconds);
                writer.WriteEndObject();
            }
        }

        [JsonProperty("album")]
        public SPAlbum Album { get; set; } = null;

        [JsonProperty("artists")]
        public IEnumerable<SPArtist> Artists { get; set; } = Enumerable.Empty<SPArtist>();

        [JsonProperty("available_markets")]
        public IEnumerable<string> AvailableMarkets { get; set; } = Enumerable.Empty<string>();

        [JsonProperty("disc_number")]
        public uint DiscNumber { get; set; } = 0;

        [JsonProperty("duration_ms")]
        [JsonConverter(typeof(SPTrackDurationConverter))]
        public TimeSpan DurationMS { get; set; } = new TimeSpan(0);

        [JsonProperty("explicit")]
        public bool Explicit { get; set; } = false;

        [JsonProperty("external_ids")]
        public Dictionary<string, string> ExternalIds { get; set; } = null;

        [JsonProperty("is_playable")]
        public bool IsPlayable { get; set; } = false;

        [JsonProperty("linked_from")]
        public SPTrack LinkedFrom { get; set; } = null;

        [JsonProperty("popularity")]
        public uint? Popularity { get; set; }

        [JsonProperty("preview_url")]
        public string PreviewUrl { get; set; }

        [JsonProperty("track_number")]
        public uint? TrackNumber { get; set; }

        [JsonProperty("type")]
        public override SPItemType Type
        {
            get
            {
                return SPItemType.Track;
            }
        }
    }
}
