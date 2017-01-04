using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SpotifyAspNetCore.Common
{
    /// <summary>
    /// The offset-based paging object is a container for a set of objects. It contains a key called items (whose value is an array of the requested objects) along with other keys like previous, next and limit that can be useful in future calls.
    /// </summary>
    public class SPPage
    {
        /// <summary>
        /// A link to the Web API endpoint returning the full result of the request.
        /// </summary>
        /// <returns>Uri</returns>
        [JsonProperty("href")]
        public Uri Href { get; set; }

        /// <summary>
        /// The requested data.
        /// </summary>
        /// <returns>IEnumerable</returns>
        [JsonProperty("items")]
        [JsonConverter(typeof(SPItemConverter))]
        public IEnumerable<SPItem> Items { get; set; } = Enumerable.Empty<SPItem>();

        /// <summary>
        /// The maximum number of items in the response (as set in the query or by default).
        /// </summary>
        /// <returns>uint</returns>
        [JsonProperty("limit")]
        public uint Limit { get; set; }

        /// <summary>
        /// URL to the next page of items.
        /// </summary>
        /// <returns>string</returns>
        [JsonProperty("next")]
        public string Next { get; set; }

        /// <summary>
        /// The offset of the items returned
        /// </summary>
        /// <returns>uint</returns>
        [JsonProperty("offset")]
        public uint Offset { get; set; }

        /// <summary>
        /// URL to the previous page of items.
        /// </summary>
        /// <returns>string</returns>
        [JsonProperty("previous")]
        public string Previous { get; set; }

        /// <summary>
        /// The total number of items available to return.
        /// </summary>
        /// <returns>uint</returns>
        [JsonProperty("total")]
        public uint Total { get; set; }
    }
}
