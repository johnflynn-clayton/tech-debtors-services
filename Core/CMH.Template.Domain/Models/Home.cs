using Cmh.Vmf.Infrastructure.Domain.Models;
using System;

namespace CMH.MobileHomeTracker.Domain.Models
{
    public class Home : DomainModel<Guid>
    {
        public string Name { get; set; }
    }
}
