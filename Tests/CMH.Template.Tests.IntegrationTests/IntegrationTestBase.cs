using Cmh.Vmf.Infrastructure.AspNet.Extensions;
using Cmh.Vmf.Infrastructure.Data;
using Cmh.Vmf.Infrastructure.Testing;
using CMH.MobileHomeTracker.Api;
using CMH.MobileHomeTracker.Api.Client;
using CMH.MobileHomeTracker.Tests.Shared;
using FakeItEasy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Data;
using System.Threading.Tasks;

namespace CMH.MobileHomeTracker.Tests.IntegrationTests
{
    public abstract class IntegrationTestBase : IntegrationTestFixture<Startup>
    {
        protected IDbConnection DbConnection { get; private set; }

        static IntegrationTestBase()
        {
            // Set fake AppSettings values since the dll.config is not accessible from the integration test.
            System.Configuration.ConfigurationManager.AppSettings["JwtSharedKey"] = Guid.NewGuid().ToString();
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            if (UseLocalDatabase)
            {
                services.AddDbConnectionFactory(Configuration.GetConnectionString("ConnectionStringName"));

            }
            else
            {
                // Wire up custom model repositories here since we won't be using a real database and need to override what was setup in Startup
                // These must be singletons for the data to persist between requests
                services.AddRepositoriesFromAssemblyContaining<Infrastructure.SampleRepository>(ServiceLifetime.Singleton);
            }
        }

        protected ISampleApiClient GetApiClient(bool includeAuthorizationHeader = true)
        {
            var httpClient = GetHttpClient(includeAuthorizationHeader);
            var client = SampleApiClient.CreateClient(httpClient);

            return client;
        }

        protected override async Task ResetDatabase()
        {
            DbConnection = ServiceProvider.GetService<IDbConnectionFactory>().Create();

            await DataStore.ClearData(DbConnection);
        }
    }
}
