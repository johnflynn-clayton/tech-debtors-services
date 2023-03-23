using CMH.MobileHomeTracker.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace CMH.MobileHomeTracker.Tests.RepositoryTests
{
    [TestFixture]
    public class SampleRepositoryTests : RepositoryTestBase
    {
        private IHomeRepository _repository;

        [SetUp]
        public void SetUp()
        {
            _repository = ServiceProvider.GetRequiredService<IHomeRepository>();
        }

        [Test]
        public async Task Get_ById_ReturnsSample()
        {
            var result = await _repository.GetAsync(Guid.NewGuid());

            Assert.IsNotNull(result);
            Assert.AreEqual("Sample", result.Name);
            Assert.AreEqual("A sample object for the api", result.Description);
        }

        [Test]
        public async Task Get_All_ReturnsSamples()
        {
            var results = await _repository.GetAllAsync();

            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);
            
            Assert.AreEqual("Sample", results[0].Name);
            Assert.AreEqual("A sample object for the api", results[0].Description);

            Assert.AreEqual("Sample 2", results[1].Name);
            Assert.AreEqual("Another sample object for the api", results[1].Description);
        }

        [Test]
        public void InsertAsync_Success()
        {
            Assert.DoesNotThrowAsync(() => _repository.InsertAsync(new Domain.Models.Home()));
        }

        [Test]
        public void UpdateAsync_Success()
        {
            Assert.DoesNotThrowAsync(() => _repository.UpdateAsync(new Domain.Models.Home()));
        }

        [Test]
        public void DeleteAsync_Success()
        {
            Assert.DoesNotThrowAsync(() => _repository.DeleteAsync(Guid.NewGuid()));
        }
    }
}
