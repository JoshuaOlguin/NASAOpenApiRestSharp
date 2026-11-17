using System.Text.Json.Serialization;
using NASAOpenApiRestSharp.Models.Common;

namespace NASAOpenApiRestSharp.Models.NeoBrowse
{
    public class NeoBrowseResponse
    {
        [JsonPropertyName("links")]
        public PageLinks Links { get; set; } = null!;

        [JsonPropertyName("near_earth_objects")]
        public List<BrowseNeo> NearEarthObjects { get; set; } = new();

        [JsonPropertyName("page")]
        public Page Page { get; set; } = null!;
    }
}
