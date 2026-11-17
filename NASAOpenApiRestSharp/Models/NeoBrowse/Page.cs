using System.Text.Json.Serialization;

namespace NASAOpenApiRestSharp.Models.NeoBrowse
{
    public class Page
    {
        [JsonPropertyName("size")]
        public int Size { get; set; }

        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("total_elements")]
        public int TotalElements { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }
    }
}
