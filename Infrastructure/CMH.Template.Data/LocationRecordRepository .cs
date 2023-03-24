using Cmh.Vmf.Infrastructure.Data;
using CMH.MobileHomeTracker.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMH.MobileHomeTracker.Domain.Models;
using Dapper;

namespace CMH.MobileHomeTracker.Data
{
    public class LocationRecordRepository : DapperRepository<Domain.Models.LocationRecord, DbModels.LocationRecord, Mapping.LocationRecordMapper, Guid>, ILocationRecordRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly Mapping.LocationRecordMapper _mapper;

        public LocationRecordRepository(IDbConnectionFactory connectionFactory, 
            Mapping.LocationRecordMapper mapper) : base(mapper, connectionFactory, "dbo")
        {
            _connectionFactory = connectionFactory;
            _mapper = mapper;
        }

        public override Task InsertAsync(Domain.Models.LocationRecord model)
        {
            var locationRecord = base.InsertAsync(model);
            // This is where you would insert to the database but since we don't have one right now we will simulate it
            return locationRecord;
        }

        public override async Task<Domain.Models.LocationRecord> GetAsync(Guid id)
        {
            var locationRecord = await base.GetAsync(id);

            return locationRecord;
        }

        public override async Task<List<Domain.Models.LocationRecord>> GetAllAsync()
        {
            var locationRecords = await base.GetAllAsync();

            return locationRecords;
        }

        public override async Task UpdateAsync(Domain.Models.LocationRecord model)
        {
            await base.UpdateAsync(model);
        }

        public override async Task DeleteAsync(Guid id)
        {
            await base.DeleteAsync(id);
        }

        public async Task<Domain.Models.LocationRecord> GetLocationForHomeId(Guid id)
        {
            using (var conn = _connectionFactory.Create())
            {
                var sql = $"select top 1 * from LocationRecord where homeId = @id order by recorddate desc";
                var location = await conn.QueryAsync<DbModels.LocationRecord>(sql, new { id });

                return _mapper.Map(location.FirstOrDefault());
            }
        }
    }
}
