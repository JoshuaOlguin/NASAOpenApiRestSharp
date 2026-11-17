using System.Text.Json.Serialization;

namespace NASAOpenApiRestSharp.Models.Common
{
    public class ErrorResponse
    {
        [JsonPropertyName("error")]
        public ApiError Error { get; set; } = null!;
    }
}
