using System.Text.Json.Serialization;
using NASAOpenApiRestSharp.Models.Common;

namespace NASAOpenApiRestSharp.Models.NeoFeed
{
    public class NeoFeedResponse
    {
        [JsonPropertyName("links")]
        public PageLinks Links { get; set; } = null!;

        [JsonPropertyName("element_count")]
        public int ElementCount { get; set; }

        [JsonPropertyName("near_earth_objects")]
        public Dictionary<string, List<NearEarthObject>> NearEarthObjects { get; set; } = new();
    }
}
