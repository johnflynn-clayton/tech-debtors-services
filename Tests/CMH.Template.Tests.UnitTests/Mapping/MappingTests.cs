using Cmh.Vmf.Infrastructure.Testing.Mapping;
using NUnit.Framework;

namespace CMH.MobileHomeTracker.Tests.UnitTests.Mapping
{
    [TestFixture]
    public class MappingTests
    {
        [Test]
        public void VerifyAutoMapperConfiguration()
        {
            Assert.DoesNotThrow(() => MappingHelper.VerifyMappers("CMH.MobileHomeTracker.*.dll"));
        }
    }
}
