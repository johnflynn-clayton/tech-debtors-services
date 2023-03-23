using Cmh.Vmf.Infrastructure.AspNet.Dto;
using System;

namespace CMH.MobileHomeTracker.Dto
{
    public class LocationRecord : DtoBase<Guid>
    {
        public Guid HomeID { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
