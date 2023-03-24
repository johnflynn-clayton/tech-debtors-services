using Cmh.Vmf.Infrastructure.AspNet.Dto;
using System;

namespace CMH.MobileHomeTracker.Dto
{
    public class Home : DtoBase<Guid>
    {
        public string Name { get; set; }
    }
}
