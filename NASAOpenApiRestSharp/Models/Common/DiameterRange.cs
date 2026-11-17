using System.Text.Json.Serialization;

namespace NASAOpenApiRestSharp.Models.Common
{
    public class DiameterRange
    {
        [JsonPropertyName("estimated_diameter_min")]
        public double EstimatedDiameterMin { get; set; }

        [JsonPropertyName("estimated_diameter_max")]
        public double EstimatedDiameterMax { get; set; }
    }
}
