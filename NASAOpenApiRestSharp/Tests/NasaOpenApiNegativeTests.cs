using NASAOpenApiRestSharp.Models.Common;
using System.Net;

namespace NASAOpenApiRestSharp.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    [Category("Negative")]
    public class NasaOpenApiNegativeTests : NasaApiTestBase
    {
        [Test(Description = "Verify Neo Feed API endpoint returns 400 Bad Request for invalid date range")]
        public async Task NeoFeed_InvalidDateRange_Returns400()
        {
            var response = await GetAsync(NeoFeedResource, new Dictionary<string, string>
            {
                ["start_date"] = "2026-10-01",
                ["end_date"] = "2026-10-20",
                ["api_key"] = ApiKey
            });

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test(Description = "Verify Neo Feed API endpoint returns 403 Forbidden for missing API key")]
        public async Task NeoFeed_MissingApiKey_Returns403()
        {
            var response = await GetAsync(NeoFeedResource, new Dictionary<string, string>
            {
                ["start_date"] = "2026-10-01",
                ["end_date"] = "2026-10-20"
            });

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            Assert.That(Deserialize<ErrorResponse>(response).Error.Code, Is.EqualTo("API_KEY_MISSING"));
        }

        [Test(Description = "Neo Feed returns 403 when api_key is invalid")]
        public async Task NeoFeed_InvalidApiKey_Returns403()
        {
            var response = await GetAsync(NeoFeedResource, new Dictionary<string, string>
            {
                ["start_date"] = "2026-10-01",
                ["end_date"] = "2026-10-20",
                ["api_key"] = "invalid_key_12345"
            });

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            Assert.That(Deserialize<ErrorResponse>(response).Error.Code, Is.EqualTo("API_KEY_INVALID"));
        }

        [Test(Description = "Verify Neo Look Up API endpoint returns http code 403 Forbidden for invalid API key")]
        public async Task NeoLookup_WithoutIdAndInvalidApiKey_Returns403()
        {
            var response = await GetAsync(NeoLookupResource, new Dictionary<string, string>
            {
                ["start_date"] = "2026-10-01",
                ["end_date"] = "2026-10-20",
                ["api_key"] = "invalid_key"
            });

            TestContext.Out.WriteLine((int)response.StatusCode);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
        }

        [Test(Description = "Verify Neo Look Up API endpoint returns http code 403 Forbidden when api_key is invalid")]
        public async Task NeoLookup_InvalidApiKey_Returns403()
        {
            const string asteroidId = "3542519";

            var response = await GetAsync($"{NeoLookupResource}/{asteroidId}", new Dictionary<string, string>
            {
                ["api_key"] = "invalid_key_12345"
            });

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            Assert.That(Deserialize<ErrorResponse>(response).Error.Code, Is.EqualTo("API_KEY_INVALID"));
        }

        [Test(Description = "Verify Neo Look Up API endpoint returns http code 404 Not Found when the asteroid id does not exist")]
        public async Task NeoLookup_NonExistentId_Returns404()
        {
            const string invalidAsteroidId = "0000000";

            var response = await GetAsync($"{NeoLookupResource}/{invalidAsteroidId}", new Dictionary<string, string>
            {
                ["api_key"] = ApiKey
            });

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.NotFound));
            Assert.That(response.IsSuccessful, Is.False);
        }

        [Test(Description = "Neo Browse returns 403 when api_key is missing")]
        public async Task NeoBrowse_MissingApiKey_Returns403()
        {
            var response = await GetAsync(NeoBrowseResource);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            Assert.That(Deserialize<ErrorResponse>(response).Error.Code, Is.EqualTo("API_KEY_MISSING"));
        }

        [Test(Description = "Neo Browse returns 403 when api_key is invalid")]
        public async Task NeoBrowse_InvalidApiKey_Returns403()
        {
            var response = await GetAsync(NeoBrowseResource, new Dictionary<string, string>
            {
                ["api_key"] = "invalid_key_12345"
            });

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            Assert.That(Deserialize<ErrorResponse>(response).Error.Code, Is.EqualTo("API_KEY_INVALID"));
        }
    }
}
