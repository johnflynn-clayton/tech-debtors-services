using Cmh.Vmf.Infrastructure.Domain.Models;
using System;

namespace CMH.MobileHomeTracker.Domain.Models
{
    public class LocationRecord : DomainModel<Guid>
    {
        public Guid HomeID { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
