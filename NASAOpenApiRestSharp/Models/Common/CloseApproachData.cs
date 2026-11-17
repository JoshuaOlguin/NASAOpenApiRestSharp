using System.Text.Json.Serialization;

namespace NASAOpenApiRestSharp.Models.Common
{
    public class CloseApproachData
    {
        [JsonPropertyName("close_approach_date")]
        public string CloseApproachDate { get; set; } = string.Empty;

        [JsonPropertyName("close_approach_date_full")]
        public string CloseApproachDateFull { get; set; } = string.Empty;

        [JsonPropertyName("epoch_date_close_approach")]
        public long EpochDateCloseApproach { get; set; }

        [JsonPropertyName("relative_velocity")]
        public RelativeVelocity RelativeVelocity { get; set; } = null!;

        [JsonPropertyName("miss_distance")]
        public MissDistance MissDistance { get; set; } = null!;

        [JsonPropertyName("orbiting_body")]
        public string OrbitingBody { get; set; } = string.Empty;
    }
}
