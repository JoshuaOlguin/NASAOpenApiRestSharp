using NASAOpenApiRestSharp.Models.NeoFeed;
using NASAOpenApiRestSharp.Models.NeoLookup;
using NASAOpenApiRestSharp.Models.NeoBrowse;
using System.Net;

namespace NASAOpenApiRestSharp.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    [Category("Positive")]
    public class NasaOpenApiPositiveTests : NasaApiTestBase
    {
        [Test(Description = "Verify Neo Feed API endpoint returns list of near earth asteroids by date range.")]
        public async Task NeoFeed_ReturnsAsteroidsByDateRange()
        {
            var response = await GetAsync(NeoFeedResource, new Dictionary<string, string>
            {
                ["start_date"] = ValidRange.Start,
                ["end_date"] = ValidRange.End,
                ["api_key"] = ApiKey
            });

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.IsSuccessful, Is.True);
            Assert.That(response.ContentType, Does.Contain("application/json"));

            var body = Deserialize<NeoFeedResponse>(response);

            Assert.That(body.ElementCount, Is.GreaterThan(0));
            Assert.That(body.NearEarthObjects, Does.ContainKey(ValidRange.Start));
            Assert.That(body.NearEarthObjects, Does.ContainKey(ValidRange.End));
            Assert.That(body.Links, Is.Not.Null);
            Assert.That(body.Links.Self, Does.Contain(ValidRange.Start));
            Assert.That(body.Links.Self, Does.Contain(ValidRange.End));
        }

        [Test(Description = "Verify Neo Lookup API endpoint fetches asteroid information by NASA JPL small body id.")]
        public async Task NeoLookup_ReturnsAsteroidById()
        {
            const string asteroidId = "3542519";
            const string designationName = "2010 PK9";

            var response = await GetAsync($"{NeoLookupResource}/{asteroidId}", new Dictionary<string, string>
            {
                ["api_key"] = ApiKey
            });

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.IsSuccessful, Is.True);
            Assert.That(response.ContentType, Does.Contain("application/json"));

            var body = Deserialize<NeoLookupResponse>(response);

            Assert.That(body.Id, Is.EqualTo(asteroidId));
            Assert.That(body.Designation, Is.EqualTo(designationName));

            Assert.Multiple(() =>
            {
                Assert.That(body.EstimatedDiameter?.Meters, Is.Not.Null);
                Assert.That(body.EstimatedDiameter?.Meters?.EstimatedDiameterMax, Is.GreaterThan(0));
                Assert.That(body.CloseApproachData, Is.Not.Empty);
                Assert.That(body.OrbitalData?.OrbitClass?.OrbitClassType, Is.Not.Null.And.Not.Empty);
            });
        }

        [Test(Description = "Verify Neo Browse API endpoint returns a paginated list of all known near earth asteroids.")]
        public async Task NeoBrowse_ReturnsPaginatedAsteroids()
        {
            var response = await GetAsync(NeoBrowseResource, new Dictionary<string, string>
            {
                ["api_key"] = ApiKey
            });

            TestContext.Out.WriteLine(response.ResponseUri);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.IsSuccessful, Is.True);
            Assert.That(response.ContentType, Does.Contain("application/json"));

            var body = Deserialize<NeoBrowseResponse>(response);

            Assert.That(body.Page.Number, Is.EqualTo(0));
            Assert.That(body.Page.Size, Is.GreaterThan(0));
            Assert.That(body.Page.TotalElements, Is.GreaterThan(0));
            Assert.That(body.Page.TotalPages, Is.EqualTo((int)Math.Ceiling((double)body.Page.TotalElements / body.Page.Size)));

            Assert.That(body.Links.Previous, Is.Null);
            Assert.That(body.Links.Next, Is.Not.Null);

            Assert.That(body.NearEarthObjects, Is.Not.Empty);
            Assert.That(body.NearEarthObjects.Count, Is.LessThanOrEqualTo(body.Page.Size));

            var ids = body.NearEarthObjects.Select(n => n.Id).ToList();
            Assert.That(ids, Is.Unique);
        }
    }
}