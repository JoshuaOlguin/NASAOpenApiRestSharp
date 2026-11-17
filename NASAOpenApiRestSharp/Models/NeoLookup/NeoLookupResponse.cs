using System.Text.Json.Serialization;
using NASAOpenApiRestSharp.Models.Common;

namespace NASAOpenApiRestSharp.Models.NeoLookup
{
    public class NeoLookupResponse
    {
        [JsonPropertyName("absolute_magnitude_h")]
        public double AbsoluteMagnitudeH { get; set; }

        [JsonPropertyName("close_approach_data")]
        public List<CloseApproachData> CloseApproachData { get; set; } = new();

        [JsonPropertyName("designation")]
        public string Designation { get; set; } = string.Empty;

        [JsonPropertyName("estimated_diameter")]
        public EstimatedDiameter EstimatedDiameter { get; set; } = null!;

        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("is_potentially_hazardous_asteroid")]
        public bool IsPotentiallyHazardousAsteroid { get; set; }

        [JsonPropertyName("is_sentry_object")]
        public bool IsSentryObject { get; set; }

        [JsonPropertyName("links")]
        public SelfLink Links { get; set; } = null!;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("nasa_jpl_url")]
        public string NasaJplUrl { get; set; } = string.Empty;

        [JsonPropertyName("neo_reference_id")]
        public string NeoReferenceId { get; set; } = string.Empty;

        [JsonPropertyName("orbital_data")]
        public OrbitalData OrbitalData { get; set; } = null!;
    }
}
