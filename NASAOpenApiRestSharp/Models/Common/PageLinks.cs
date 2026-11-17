using System.Text.Json.Serialization;

namespace NASAOpenApiRestSharp.Models.Common
{
    public class PageLinks
    {
        [JsonPropertyName("next")]
        public string? Next { get; set; }

        [JsonPropertyName("previous")]
        public string? Previous { get; set; }

        [JsonPropertyName("self")]
        public string Self { get; set; } = string.Empty;
    }
}
