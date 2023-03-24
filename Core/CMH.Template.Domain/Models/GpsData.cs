using System;
using System.Collections.Generic;
using System.Text;

namespace CMH.MobileHomeTracker.Domain.Models
{
    public class GpsData
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTime Date { get; set; }
        public double Elevation { get; set; }
    }
}
