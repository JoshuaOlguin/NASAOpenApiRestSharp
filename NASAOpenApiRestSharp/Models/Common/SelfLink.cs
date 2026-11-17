using System.Text.Json.Serialization;

namespace NASAOpenApiRestSharp.Models.Common
{
    public class SelfLink
    {
        [JsonPropertyName("self")]
        public string Self { get; set; } = string.Empty;
    }
}
