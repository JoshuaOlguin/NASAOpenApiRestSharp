using System.Text.Json.Serialization;

namespace NASAOpenApiRestSharp.Models.Common
{
    public class EstimatedDiameter
    {
        [JsonPropertyName("kilometers")]
        public DiameterRange Kilometers { get; set; } = null!;

        [JsonPropertyName("meters")]
        public DiameterRange Meters { get; set; } = null!;

        [JsonPropertyName("miles")]
        public DiameterRange Miles { get; set; } = null!;

        [JsonPropertyName("feet")]
        public DiameterRange Feet { get; set; } = null!;
    }
}
