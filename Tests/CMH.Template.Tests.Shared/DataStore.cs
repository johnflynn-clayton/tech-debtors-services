using System.Data;
using System.Threading.Tasks;

namespace CMH.MobileHomeTracker.Tests.Shared
{
    /// <summary>
    /// Any known data needed for tests should come from here
    /// </summary>
    public static class DataStore
    {
        /// <summary>
        /// This function should be called before each RepositoryTest and IntegrationTest to reset the local database
        /// to a known state prior to each test being executed
        /// </summary>
        public static async Task ClearData(IDbConnection connection)
        {
            // data must be cleared in correct order to not violate foreign key references
            await Task.CompletedTask;
        }
    }
}
