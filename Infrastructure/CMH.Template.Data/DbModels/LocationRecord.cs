using System;

namespace CMH.MobileHomeTracker.Data.DbModels
{
    public class LocationRecord : DbModel<Guid>
    {
        public Guid HomeID { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
