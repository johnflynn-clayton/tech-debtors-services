using Cmh.Vmf.Infrastructure.AspNet.Extensions;
using Cmh.Vmf.Infrastructure.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CMH.MobileHomeTracker.Tests.IntegrationTests
{
    [TestFixture]
    public class HealthTests : IntegrationTestBase
    {
        [Test]
        public async Task Liveness_Success()
        {
            var client = GetHttpClient();
            var response = await client.GetAsync("/health/live");

            Assert.IsNotNull(response);
            Assert.IsTrue(response.IsSuccessStatusCode);

            var healthCheckResponse = await response.FromJson<HealthCheckResponse>();

            Assert.IsNotNull(healthCheckResponse);
            Assert.AreEqual(HealthStatus.Healthy.ToString(), healthCheckResponse.Status);
        }
    }
}
