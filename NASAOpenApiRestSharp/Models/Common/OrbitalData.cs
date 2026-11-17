using System.Text.Json.Serialization;

namespace NASAOpenApiRestSharp.Models.Common
{
    public class OrbitalData
    {
        [JsonPropertyName("aphelion_distance")]
        public string AphelionDistance { get; set; } = string.Empty;

        [JsonPropertyName("ascending_node_longitude")]
        public string AscendingNodeLongitude { get; set; } = string.Empty;

        [JsonPropertyName("data_arc_in_days")]
        public int DataArcInDays { get; set; }

        [JsonPropertyName("eccentricity")]
        public string Eccentricity { get; set; } = string.Empty;

        [JsonPropertyName("epoch_osculation")]
        public string EpochOsculation { get; set; } = string.Empty;

        [JsonPropertyName("equinox")]
        public string Equinox { get; set; } = string.Empty;

        [JsonPropertyName("first_observation_date")]
        public string FirstObservationDate { get; set; } = string.Empty;

        [JsonPropertyName("inclination")]
        public string Inclination { get; set; } = string.Empty;

        [JsonPropertyName("jupiter_tisserand_invariant")]
        public string JupiterTisserandInvariant { get; set; } = string.Empty;

        [JsonPropertyName("last_observation_date")]
        public string LastObservationDate { get; set; } = string.Empty;

        [JsonPropertyName("mean_anomaly")]
        public string MeanAnomaly { get; set; } = string.Empty;

        [JsonPropertyName("mean_motion")]
        public string MeanMotion { get; set; } = string.Empty;

        [JsonPropertyName("minimum_orbit_intersection")]
        public string MinimumOrbitIntersection { get; set; } = string.Empty;

        [JsonPropertyName("observations_used")]
        public int ObservationsUsed { get; set; }

        [JsonPropertyName("orbit_class")]
        public OrbitClass OrbitClass { get; set; } = null!;

        [JsonPropertyName("orbit_determination_date")]
        public string OrbitDeterminationDate { get; set; } = string.Empty;

        [JsonPropertyName("orbit_id")]
        public string OrbitId { get; set; } = string.Empty;

        [JsonPropertyName("orbit_uncertainty")]
        public string OrbitUncertainty { get; set; } = string.Empty;

        [JsonPropertyName("orbital_period")]
        public string OrbitalPeriod { get; set; } = string.Empty;

        [JsonPropertyName("perihelion_argument")]
        public string PerihelionArgument { get; set; } = string.Empty;

        [JsonPropertyName("perihelion_distance")]
        public string PerihelionDistance { get; set; } = string.Empty;

        [JsonPropertyName("perihelion_time")]
        public string PerihelionTime { get; set; } = string.Empty;

        [JsonPropertyName("semi_major_axis")]
        public string SemiMajorAxis { get; set; } = string.Empty;
    }
}
