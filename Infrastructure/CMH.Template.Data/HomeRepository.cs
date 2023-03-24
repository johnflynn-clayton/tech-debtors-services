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
    public class HomeRepository : DapperRepository<Domain.Models.Home, DbModels.Home, Mapping.HomeMapper, Guid>, IHomeRepository
    {
        private IDbConnectionFactory _connectionFactory;

        public HomeRepository(IDbConnectionFactory connectionFactory, Mapping.HomeMapper mapper) : base(mapper, connectionFactory, "dbo")
        {
            _connectionFactory = connectionFactory;
        }

        public override Task InsertAsync(Domain.Models.Home model)
        {
            // This is where you would insert to the database but since we don't have one right now we will simulate it
            return Task.CompletedTask;
        }

        public override async Task<Domain.Models.Home> GetAsync(Guid id)
        {
            var home = await base.GetAsync(id);

            return home;
        }

        public override async Task<List<Domain.Models.Home>> GetAllAsync()
        {
            var homes = await base.GetAllAsync();

            return homes;
        }

        public override async Task UpdateAsync(Domain.Models.Home model)
        {
            await base.UpdateAsync(model);
        }

        public override async Task DeleteAsync(Guid id)
        {
            await base.DeleteAsync(id);
        }

        public async Task<LocationRecord> GetLocationRecordForHomeId(Guid id)
        {
            using (var connection = _connectionFactory.Create())
            {
                var sql = "select * from LocationRecord where HomeId = @id";
                var records = await connection.QueryAsync<LocationRecord>(sql, new { id });

                return records.FirstOrDefault();
            }

        }
    }
}
