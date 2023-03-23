using Cmh.Vmf.Infrastructure.AspNet.Extensions;
using Cmh.Vmf.Infrastructure.Testing;
using CMH.MobileHomeTracker.Tests.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace CMH.MobileHomeTracker.Tests.RepositoryTests
{
    [TeamCityIgnore]
    public abstract class RepositoryTestBase : RepositoryTestFixture
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddDbConnectionFactory(Configuration.GetConnectionString("ConnectionStringName"));
            services.AddRepositoriesFromAssemblyContaining<Data.HomeRepository>();
            services.AddMappersFromAssemblyContaining<Data.Mapping.SampleMapper>();
        }

        protected override async Task ResetDatabase()
        {
            await DataStore.ClearData(DbConnection);
        }
    }
}
