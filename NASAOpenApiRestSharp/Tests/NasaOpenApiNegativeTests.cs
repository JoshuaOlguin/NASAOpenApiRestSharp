using NASAOpenApiRestSharp.Models.Common;
using RestSharp;
using System.Net;

namespace NASAOpenApiRestSharp.Tests
{
    /// <summary>
    /// Contains negative integration tests for NASA Open APIs.
    /// </summary>
    /// <remarks>
    /// Verifies that invalid requests return the expected HTTP status codes
    /// and error response codes for the Neo Feed, Neo Lookup, and Neo Browse endpoints.
    /// </remarks>
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    [Category("Negative")]
    public class NasaOpenApiNegativeTests : NasaApiTestBase
    {
        /// <summary>
        /// Verifies that the Neo Feed endpoint returns <see cref="HttpStatusCode.BadRequest"/>
        /// when the requested date range is invalid.
        /// </summary>
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

        /// <summary>
        /// Verifies that the Neo Feed endpoint returns <see cref="HttpStatusCode.Forbidden"/>
        /// with an <c>API_KEY_MISSING</c> error when no API key is supplied.
        /// </summary>
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

        /// <summary>
        /// Verifies that the Neo Feed endpoint returns <see cref="HttpStatusCode.Forbidden"/>
        /// with an <c>API_KEY_INVALID</c> error when an invalid API key is supplied.
        /// </summary>
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

        /// <summary>
        /// Verifies that the Neo Feed endpoint rejects invalid start-date formats and values
        /// with a client-error HTTP status code.
        /// </summary>
        /// <param name="invalidDate">The invalid start date submitted to the endpoint.</param>
        /// <param name="description">Description of the invalid date format or value.</param>
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

        /// <summary>
        /// Verifies that the Neo Lookup endpoint returns <see cref="HttpStatusCode.Forbidden"/>
        /// when an invalid API key is supplied without an asteroid identifier.
        /// </summary>
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

        /// <summary>
        /// Verifies that the Neo Lookup endpoint returns <see cref="HttpStatusCode.Forbidden"/>
        /// with an <c>API_KEY_INVALID</c> error for an invalid API key.
        /// </summary>
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

        /// <summary>
        /// Verifies that the Neo Lookup endpoint returns <see cref="HttpStatusCode.NotFound"/>
        /// when the requested asteroid identifier does not exist.
        /// </summary>
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

        /// <summary>
        /// Verifies that the Neo Browse endpoint returns <see cref="HttpStatusCode.Forbidden"/>
        /// with an <c>API_KEY_MISSING</c> error when no API key is supplied.
        /// </summary>
        [Test(Description = "Neo Browse returns 403 when api_key is missing")]
        public async Task NeoBrowse_MissingApiKey_Returns403()
        {
            var response = await GetAsync(NeoBrowseResource);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
            Assert.That(Deserialize<ErrorResponse>(response).Error.Code, Is.EqualTo("API_KEY_MISSING"));
        }

        /// <summary>
        /// Verifies that the Neo Browse endpoint returns <see cref="HttpStatusCode.Forbidden"/>
        /// with an <c>API_KEY_INVALID</c> error when an invalid API key is supplied.
        /// </summary>
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
