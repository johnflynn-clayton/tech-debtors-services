using Cmh.Vmf.Infrastructure.Common.Extensions;
using Cmh.Vmf.Infrastructure.Domain;
using Cmh.Vmf.Infrastructure.Domain.Exceptions;
using Cmh.Vmf.Infrastructure.Domain.Services;
using CMH.MobileHomeTracker.Domain.Models;
using CMH.MobileHomeTracker.Domain.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CMH.MobileHomeTracker.Domain.Services
{
    public interface IHomeService : IDomainService<Models.Home, Guid>
    {
        Task<LocationRecord> GetCurrentLocationForHomeId(Guid id);
    }

    public class HomeService : DomainService<Models.Home, Guid>, IHomeService
    {
        private readonly ILogger _logger;
        private readonly IHomeRepository _repository;

        public HomeService(ILogger<HomeService> logger, IIdGenerator<Guid> idGenerator, IHomeRepository repository) : base(idGenerator, repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public override async Task<Home> CreateAsync(Home model)
        {
            _logger.LogDebug($"{nameof(HomeService)}.{nameof(CreateAsync)} with '{model.ToJson()}'");

            var ret = await base.CreateAsync(model);

            return ret;
        }

        public override async Task<Home> UpdateAsync(Home model)
        {
            _logger.LogDebug($"{nameof(HomeService)}.{nameof(UpdateAsync)} with id '{model.Id}'");

            var ret = await base.UpdateAsync(model);

            return ret;
        }

        public override async Task DeleteAsync(Guid id)
        {
            _logger.LogDebug($"{nameof(HomeService)}.{nameof(DeleteAsync)} with id '{id}'");

            await base.DeleteAsync(id);
        }

        public override async Task<Models.Home> GetAsync(Guid id)
        {
            // Simulate a not found error
            if (id == Guid.Parse("3a047b7e-7620-4def-ad2f-b1388993debb"))
            {
                throw new NotFoundException<Guid>(typeof(Models.Home), id);
            }

            return await base.GetAsync(id);
        }

        public async Task<LocationRecord> GetCurrentLocationForHomeId(Guid id)
        {
            return await _repository.GetLocationRecordForHomeId(id);
        }
    }
}
