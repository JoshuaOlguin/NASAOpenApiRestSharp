using System.Text.Json.Serialization;

namespace NASAOpenApiRestSharp.Models.Common
{
    public class RelativeVelocity
    {
        [JsonPropertyName("kilometers_per_second")]
        public string KilometersPerSecond { get; set; } = string.Empty;

        [JsonPropertyName("kilometers_per_hour")]
        public string KilometersPerHour { get; set; } = string.Empty;

        [JsonPropertyName("miles_per_hour")]
        public string MilesPerHour { get; set; } = string.Empty;
    }
}
