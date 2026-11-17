using System.Text.Json.Serialization;
using NASAOpenApiRestSharp.Models.Common;

namespace NASAOpenApiRestSharp.Models.NeoFeed
{
    public class NearEarthObject
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("neo_reference_id")]
        public string NeoReferenceId { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("nasa_jpl_url")]
        public string NasaJplUrl { get; set; } = string.Empty;

        [JsonPropertyName("absolute_magnitude_h")]
        public double AbsoluteMagnitudeH { get; set; }

        [JsonPropertyName("estimated_diameter")]
        public EstimatedDiameter EstimatedDiameter { get; set; } = null!;

        [JsonPropertyName("is_potentially_hazardous_asteroid")]
        public bool IsPotentiallyHazardousAsteroid { get; set; }

        [JsonPropertyName("is_sentry_object")]
        public bool IsSentryObject { get; set; }

        [JsonPropertyName("close_approach_data")]
        public List<CloseApproachData> CloseApproachData { get; set; } = new();

        [JsonPropertyName("links")]
        public SelfLink Links { get; set; } = null!;
    }
}
