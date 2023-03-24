using Cmh.Vmf.Infrastructure.Common.Extensions;
using Cmh.Vmf.Infrastructure.Domain;
using Cmh.Vmf.Infrastructure.Domain.Services;
using CMH.MobileHomeTracker.Domain.Models;
using CMH.MobileHomeTracker.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using CMH.MobileHomeTracker.Domain.Adapters;

namespace CMH.MobileHomeTracker.Domain.Services
{
    public interface ILocationRecordService : IDomainService<Models.LocationRecord, Guid>
    {
        Task<Models.LocationRecord> GetLocationForHomeId(Guid homeId);
    }

    public class LocationRecordService : DomainService<Models.LocationRecord, Guid>, ILocationRecordService
    {
        private readonly ILogger _logger;
        private readonly IGitHubAdapter _gitHubAdapter;
        private readonly ILocationRecordRepository _repository;
        private readonly IIdGenerator<Guid> _idGenerator;

        public LocationRecordService(ILogger<LocationRecordService> logger, 
            IIdGenerator<Guid> idGenerator, 
            ILocationRecordRepository repository,
            IGitHubAdapter gitHubAdapter) : base(idGenerator, repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _gitHubAdapter = gitHubAdapter ?? throw new ArgumentNullException(nameof(gitHubAdapter));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _idGenerator = idGenerator;
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

        public async Task<Models.LocationRecord> GetLocationForHomeId(Guid homeId)
        {
            var location = await _gitHubAdapter.GetLocationDataForId(homeId);
            var ret = new Models.LocationRecord
            {
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                RecordDate = location.Date,
                HomeID = homeId
            };

            var dbLocation = await _repository.GetLocationForHomeId(homeId);

            if (dbLocation == null || location.Date > dbLocation.RecordDate)
            {
                var record = new Models.LocationRecord();

                record.Longitude = location.Longitude;
                record.Latitude = location.Latitude;
                record.RecordDate = location.Date;
                record.HomeID = homeId;
                record.Id = _idGenerator.GetNextId();

                await base.CreateAsync(record);
            }

            return ret;
        }
    }
}
