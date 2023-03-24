using CMH.MobileHomeTracker.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMH.MobileHomeTracker.Domain.Adapters
{
    public interface IGitHubAdapter
    {
        Task<GpsData> GetLocationDataForId(Guid id);
    }
}
