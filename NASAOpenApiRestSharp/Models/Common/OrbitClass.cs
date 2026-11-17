using System.Text.Json.Serialization;

namespace NASAOpenApiRestSharp.Models.Common
{
    public class OrbitClass
    {
        [JsonPropertyName("orbit_class_type")]
        public string OrbitClassType { get; set; } = string.Empty;

        [JsonPropertyName("orbit_class_description")]
        public string OrbitClassDescription { get; set; } = string.Empty;

        [JsonPropertyName("orbit_class_range")]
        public string OrbitClassRange { get; set; } = string.Empty;
    }
}
