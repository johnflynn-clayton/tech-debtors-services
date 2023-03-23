using Cmh.Vmf.Infrastructure.Domain.Repositories;
using System;

namespace CMH.MobileHomeTracker.Domain.Repositories
{
    public interface IHomeRepository : IRepository<Models.Home, Guid>
    {
    }
}
