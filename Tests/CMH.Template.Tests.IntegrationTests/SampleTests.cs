using Cmh.Vmf.Infrastructure.AspNet.Exceptions;
using Cmh.Vmf.Infrastructure.RestClient;
using CMH.MobileHomeTracker.Api.Client;
using CMH.MobileHomeTracker.Dto;
using FakeItEasy;
using NUnit.Framework;
using System;
using System.Net;
using System.Threading.Tasks;

namespace CMH.MobileHomeTracker.Tests.IntegrationTests
{
    [TestFixture]
    public class SampleTests : IntegrationTestBase
    {
        private ISampleApiClient _client;
        private RestClient _restClient;

        [SetUp]
        public void SetUp()
        {
            _client = GetApiClient();
            _restClient = (RestClient)_client;
        }

        [Test]
        public async Task Create_Success()
        {
            var dto = GetValidSample();
            var created = await CreateSample(dto);

            _restClient.ResponseProcessor = response => Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var returned = await _client.SampleGetByIdAsync(created.Id);

            Assert.IsNotNull(returned);
            Assert.AreEqual(dto.Name, returned.Name);
            Assert.AreEqual(dto.Description, returned.Description);
            Assert.AreNotEqual(Guid.Empty, returned.Id);
        }

        [Test]
        public void Create_InvalidName()
        {
            var dto = GetValidSample();

            dto.Name = dto.Name.PadRight(26, 'z');

            var exception = Assert.ThrowsAsync<ApiRequestException>(async () => await _client.SampleAddAsync(dto));

            Assert.IsNotNull(exception);
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Response.StatusCode);
            Assert.IsTrue(exception.Message.Contains($"The length of '{nameof(Home.Name)}' must be 25 characters or fewer. You entered 26 characters."));
        }

        [Test]
        public void Create_InvalidDescription()
        {
            var dto = GetValidSample();

            dto.Description = dto.Description.PadRight(101, 'z');

            var exception = Assert.ThrowsAsync<ApiRequestException>(async () => await _client.SampleAddAsync(dto));

            Assert.IsNotNull(exception);
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Response.StatusCode);
            Assert.IsTrue(exception.Message.Contains($"The length of '{nameof(Home.Description)}' must be 100 characters or fewer. You entered 101 characters."));
        }

        [Test]
        public void Create_NotAuthorized()
        {
            var client = GetApiClient(includeAuthorizationHeader: false);
            var dto = GetValidSample();
            var exception = Assert.ThrowsAsync<ApiRequestException>(async () => await _client.SampleAddAsync(dto));

            Assert.IsNotNull(exception);
            Assert.AreEqual(HttpStatusCode.Unauthorized, exception.Response.StatusCode);
        }

        [Test]
        public void GetById_NotFound()
        {
            var id = Guid.Parse("3a047b7e-7620-4def-ad2f-b1388993debb");
            var exception = Assert.ThrowsAsync<ApiRequestException>(async () => await _client.SampleGetByIdAsync(id));

            Assert.IsNotNull(exception);
            Assert.AreEqual(HttpStatusCode.NotFound, exception.Response.StatusCode);
        }

        [Test]
        public void GetById_NotAuthorized()
        {
            var client = GetApiClient(includeAuthorizationHeader: false);
            var exception = Assert.ThrowsAsync<ApiRequestException>(async () => await _client.SampleGetByIdAsync(Guid.NewGuid()));

            Assert.IsNotNull(exception);
            Assert.AreEqual(HttpStatusCode.Unauthorized, exception.Response.StatusCode);
        }

        [Test]
        public void GetById_InvalidGuid()
        {
            var exception = Assert.ThrowsAsync<ApiRequestException>(async () => await _client.SampleGetByIdAsync(Guid.Empty));

            Assert.IsNotNull(exception);
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Response.StatusCode);
        }

        [Test]
        public async Task Update_Success()
        {
            var dto = GetValidSample();
            var created = await CreateSample(dto);

            created.Name = "Updated";
            created.Description = "Updated Description";

            _restClient.ResponseProcessor = response => Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            await _client.SampleUpdateAsync(created);

            _restClient.ResponseProcessor = null;
            var returned = await _client.SampleGetByIdAsync(created.Id);

            Assert.AreEqual(created.Name, returned.Name);
            Assert.AreEqual(created.Description, returned.Description);
        }

        [Test]
        public void Update_InvalidName()
        {
            var dto = GetValidSample();

            dto.Name = dto.Name.PadRight(26, 'z');

            var exception = Assert.ThrowsAsync<ApiRequestException>(async () => await _client.SampleUpdateAsync(dto));

            Assert.IsNotNull(exception);
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Response.StatusCode);
            Assert.IsTrue(exception.Message.Contains($"The length of '{nameof(Home.Name)}' must be 25 characters or fewer. You entered 26 characters."));
        }

        [Test]
        public void Update_InvalidDescription()
        {
            var dto = GetValidSample();

            dto.Description = dto.Description.PadRight(101, 'z');

            var exception = Assert.ThrowsAsync<ApiRequestException>(async () => await _client.SampleUpdateAsync(dto));

            Assert.IsNotNull(exception);
            Assert.AreEqual(HttpStatusCode.BadRequest, exception.Response.StatusCode);
            Assert.IsTrue(exception.Message.Contains($"The length of '{nameof(Home.Description)}' must be 100 characters or fewer. You entered 101 characters."));
        }

        [Test]
        public void Update_NotAuthorized()
        {
            var client = GetApiClient(includeAuthorizationHeader: false);
            var exception = Assert.ThrowsAsync<ApiRequestException>(async () => await _client.SampleUpdateAsync(GetValidSample()));

            Assert.IsNotNull(exception);
            Assert.AreEqual(HttpStatusCode.Unauthorized, exception.Response.StatusCode);
        }

        [Test]
        public async Task Delete_Success()
        {
            var dto = GetValidSample();
            var created = await CreateSample(dto);

            _restClient.ResponseProcessor = response => Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
            await _client.SampleDeleteAsync(created.Id);

            _restClient.ResponseProcessor = null;
            var exception = Assert.ThrowsAsync<ApiRequestException>(async () => await _client.SampleGetByIdAsync(created.Id));

            Assert.IsNotNull(exception);
            Assert.AreEqual(HttpStatusCode.NotFound, exception.Response.StatusCode);
        }

        [Test]
        public void Delete_NotAuthorized()
        {
            var client = GetApiClient(includeAuthorizationHeader: false);
            var exception = Assert.ThrowsAsync<ApiRequestException>(async () => await _client.SampleDeleteAsync(Guid.NewGuid()));

            Assert.IsNotNull(exception);
            Assert.AreEqual(HttpStatusCode.Unauthorized, exception.Response.StatusCode);
        }

        [Test]
        public void Delete_NotAGuid()
        {
            var client = GetApiClient(includeAuthorizationHeader: false);
            var exception = Assert.ThrowsAsync<ApiRequestException>(async () => await _client.SampleDeleteAsync(Guid.Empty));

            Assert.IsNotNull(exception);
            Assert.AreEqual(HttpStatusCode.Unauthorized, exception.Response.StatusCode);
        }

        private async Task<Home> CreateSample(Home dto)
        {
            _restClient.ResponseProcessor = response => Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var response = await _client.SampleAddAsync(dto);

            return response;
        }

        private Home GetValidSample()
        {
            return new Home
            {
                Name = "Sample",
                Description = "A sample object for the api"
            };
        }
    }
}
