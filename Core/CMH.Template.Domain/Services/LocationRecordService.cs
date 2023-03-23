using Cmh.Vmf.Infrastructure.Common.Extensions;
using Cmh.Vmf.Infrastructure.Domain;
using Cmh.Vmf.Infrastructure.Domain.Services;
using CMH.MobileHomeTracker.Domain.Models;
using CMH.MobileHomeTracker.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CMH.MobileHomeTracker.Domain.Services
{
    public interface ILocationRecordService : IDomainService<Models.LocationRecord, Guid>
    {
    }

    public class LocationRecordService : DomainService<Models.LocationRecord, Guid>, ILocationRecordService
    {
        private readonly ILogger _logger;

        public LocationRecordService(ILogger<LocationRecordService> logger, IIdGenerator<Guid> idGenerator, ILocationRecordRepository repository) : base(idGenerator, repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override async Task<LocationRecord> CreateAsync(LocationRecord model)
        {
            _logger.LogDebug($"{nameof(LocationRecordService)}.{nameof(CreateAsync)} with '{model.ToJson()}'");

            var ret = await base.CreateAsync(model);

            return ret;
        }

        public override async Task<LocationRecord> UpdateAsync(LocationRecord model)
        {
            _logger.LogDebug($"{nameof(HomeService)}.{nameof(UpdateAsync)} with id '{model.Id}'");

            var ret = await base.UpdateAsync(model);

            return ret;
        }

        public override async Task DeleteAsync(Guid id)
        {
            _logger.LogDebug($"{nameof(LocationRecordService)}.{nameof(DeleteAsync)} with id '{id}'");

            await base.DeleteAsync(id);
        }

        public override async Task<Models.LocationRecord> GetAsync(Guid id)
        {
            return await base.GetAsync(id);
        }
    }
}
