using System.Text.Json.Serialization;

namespace NASAOpenApiRestSharp.Models.Common
{
    public class ApiError
    {
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
    }
}
