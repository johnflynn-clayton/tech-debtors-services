using Cmh.Vmf.Infrastructure.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace CMH.MobileHomeTracker.Domain.Repositories
{
    public interface ILocationRecordRepository : IRepository<Models.LocationRecord, Guid>
    {
        Task<Domain.Models.LocationRecord> GetLocationForHomeId(Guid id);
    }
}
