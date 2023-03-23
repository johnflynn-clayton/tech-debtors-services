using Cmh.Vmf.Infrastructure.RestClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CMH.MobileHomeTracker.Dto;

namespace CMH.MobileHomeTracker.Api.Client
{
    /// <summary>
    /// A sample api client
    /// </summary>
    public interface IMobileHomeTrackerApiClient
    {
        /// <summary>
        /// Get all Records
        /// </summary>
        Task<List<Dto.LocationRecord>> GetAllAsync();

        /// <summary>
        /// Get a single Record by id
        /// </summary>
        Task<Dto.LocationRecord> GetByHomeIdAsync(Guid id);
    }

    /// <summary>
    /// Helper methods for the configuring the api client
    /// </summary>
    public static class SampleApiClientExtensions
    {
        /// <summary>
        /// Helper to add the HttpClient for the Api with optional headers.
        /// </summary>
        public static IHttpClientBuilder AddMobileHomeTrackerApiClient(this IServiceCollection services, IConfiguration configuration, IDictionary<string, string> defaultHeaders = null, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            const string configurationName = "MobileHomeTrackerApi";
            var settings = configuration.GetRestClientSettings(configurationName);

            services.Add(new ServiceDescriptor(typeof(IMobileHomeTrackerApiClient), typeof(MobileHomeTrackerApiClient), lifetime));

            return services.AddNamedHttpClient(settings, $"{configurationName.ToLower()}-client", defaultHeaders)
                .AddHttpMessageHandler<TimingHandler>()
                .AddHttpMessageHandler(provider => provider.GetAuthorizationHandler(settings));
        }
    }

    /// <inheriddoc/>
    public class MobileHomeTrackerApiClient : RestClient, IMobileHomeTrackerApiClient
    {
        private const string _homeRecordEndpoing = "/api/home";
        private const string _locationRecordEndpoint = "/api/LocationRecord";

        /// <summary>
        /// This constructor will be used by the extension method to configure the client
        /// </summary>
        public MobileHomeTrackerApiClient(IHttpClientFactory factory) : base(factory, "mobilehometrackerapi-client")
        {
        }

        /// <summary>
        /// This constructor should only be used in the context of integration tests
        /// </summary>
        protected MobileHomeTrackerApiClient(HttpClient client) : base(client)
        {
        }

        /// <summary>
        /// Factory method used to be able to construct the client for testing purposes
        /// </summary>
        public static IMobileHomeTrackerApiClient CreateClient(HttpClient client) => new MobileHomeTrackerApiClient(client);

        /// <inheriddoc/>
        public async Task<List<Dto.LocationRecord>> GetAllAsync()
        {
            return await GetAsync<List<Dto.LocationRecord>>(_locationRecordEndpoint);
        }

        public Task<LocationRecord> GetByHomeIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
