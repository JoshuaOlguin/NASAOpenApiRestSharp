using System.Text.Json.Serialization;

namespace NASAOpenApiRestSharp.Models.Common
{
    public class MissDistance
    {
        [JsonPropertyName("astronomical")]
        public string Astronomical { get; set; } = string.Empty;

        [JsonPropertyName("lunar")]
        public string Lunar { get; set; } = string.Empty;

        [JsonPropertyName("kilometers")]
        public string Kilometers { get; set; } = string.Empty;

        [JsonPropertyName("miles")]
        public string Miles { get; set; } = string.Empty;
    }
}
