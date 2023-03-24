using Cmh.Vmf.Infrastructure.Domain.Repositories;
using System;
using System.Threading.Tasks;
using CMH.MobileHomeTracker.Domain.Models;

namespace CMH.MobileHomeTracker.Domain.Repositories
{
    public interface IHomeRepository : IRepository<Models.Home, Guid>
    {
        Task<LocationRecord> GetLocationRecordForHomeId(Guid id);
    }
}
