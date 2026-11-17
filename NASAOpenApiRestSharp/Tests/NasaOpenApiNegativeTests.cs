using NASAOpenApiRestSharp.Models.Common;
using RestSharp;
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


        [TestCase("28-10-2026", "DD-MM-YYYY format")]
        [TestCase("2026/10/28", "slash separators")]
        [TestCase("abc", "non-date text")]
        [TestCase("2026-13-01", "month out of range")]
        [TestCase("2026-02-30", "non-existent date (Feb 30)")]
        public async Task NeoFeed_RejectsInvalidStartDate(string invalidDate, string description)
        {
            var request = new RestRequest(string.Empty, Method.Get)
                .AddQueryParameter("start_date", invalidDate)
                .AddQueryParameter("end_date", ValidRange.End)
                .AddQueryParameter("api_key", ApiKey);

            var response = await Client.ExecuteAsync(request);

            var statusCode = (int)response.StatusCode;

            Assert.That(statusCode, Is.GreaterThanOrEqualTo(400),
                $"Se esperaba un error 4xx para '{description}' pero se recibió {statusCode}");
            Assert.That(statusCode, Is.LessThan(500),
                $"Se esperaba un error 4xx (no 5xx) para '{description}' pero se recibió {statusCode}");
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
