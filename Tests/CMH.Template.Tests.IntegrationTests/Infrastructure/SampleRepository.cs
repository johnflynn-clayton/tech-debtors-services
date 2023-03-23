using Cmh.Vmf.Infrastructure.Testing;
using System;

namespace CMH.MobileHomeTracker.Tests.IntegrationTests.Infrastructure
{
    public class SampleRepository : ModelRepository<Domain.Models.Home, Guid>, Domain.Repositories.IHomeRepository
    {
    }
}
