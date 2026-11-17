using RestSharp;
using System.Text.Json;

namespace NASAOpenApiRestSharp.Tests
{

    public abstract class NasaApiTestBase
    {
        protected const string BaseUrl = "https://api.nasa.gov/neo/rest/v1";
        protected const string NeoFeedResource = "feed";
        protected const string NeoLookupResource = "neo";
        protected const string NeoBrowseResource = "neo/browse";
        protected static readonly (string Start, string End) ValidRange = ("2026-10-01", "2026-10-07");
        protected static readonly string ApiKey = "C7jh7pPCpfBBRzz9jQnkg6wFdD6HqprnL9JwrWqB";

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        protected RestClient Client { get; private set; } = null!;
    
        [OneTimeSetUp]
        public void CreateClient() => Client = new RestClient(BaseUrl);
    
        [OneTimeTearDown]
        public void DisposeClient() => Client?.Dispose();
    
        
        protected async Task<RestResponse> GetAsync(string resource, IDictionary<string, string>? queryParams = null)
        {
            var request = new RestRequest(resource, Method.Get);
    
            if (queryParams is not null)
            {
                foreach (var (name, value) in queryParams)
                    request.AddQueryParameter(name, value);
            }
    
            return await Client.ExecuteAsync(request);
        }

        protected static T Deserialize<T>(RestResponse response)
        {
            Assert.That(response.Content, Is.Not.Null.And.Not.Empty, "La respuesta no tiene cuerpo.");

            var result = JsonSerializer.Deserialize<T>(response.Content!, JsonOptions);
            Assert.That(result, Is.Not.Null, $"No se pudo deserializar la respuesta a {typeof(T).Name}.");

            return result!;
        }
    }
}

