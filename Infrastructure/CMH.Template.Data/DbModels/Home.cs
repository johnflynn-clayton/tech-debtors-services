﻿using System;

namespace CMH.MobileHomeTracker.Data.DbModels
{
    public class Home : DbModel<Guid>
    {
        public string Model { get; set; }
    }
}
