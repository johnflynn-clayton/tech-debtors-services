using CMH.MobileHomeTracker.Domain.Events;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace CMH.MobileHomeTracker.Events
{
    public static class EventExtensions
    {
        public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration)
        {
            var rabbitMqSettings = configuration.GetSection("RabbitMQ")
                .Get<RabbitMqSettings>();

            services.AddSingleton<IRabbitMQConfiguration>(GetRabbitMqConfiguration(rabbitMqSettings));
            services.AddSingleton<IRabbitMQConnection, RabbitMQConnection>();
            services.AddSingleton<IEventProducer, RabbitMQProducer>();
            services.AddSingleton<IEventPublisher, EventPublisher>();

            return services;
        }

        private static RabbitMQConfiguration GetRabbitMqConfiguration(RabbitMqSettings settings)
        {
            var rabbitUser = ConfigurationManager.AppSettings["RabbitMQUsername"];
            var rabbitPassword = ConfigurationManager.AppSettings["RabbitMQPassword"];

            return new RabbitMQConfiguration()
            {
                Exchange = settings.ExchangeName,
                HostName = settings.HostName,
                UserName = settings.UserName,
                Password = settings.Password,
                Port = settings.Port,
                SslEnabled = settings.SslEnabled,
                VirtualHost = settings.VirtualHost,
                PrefetchSize = settings.PrefetchSize,
                PrefetchCount = settings.PrefetchCount,
                NetworkRecoveryIntervalMilliseconds = settings.NetworkRecoveryIntervalMilliseconds,
                ContinuationTimeoutMilliseconds = settings.ContinuationTimeoutMilliseconds,
                MaxNumberOfRetries = settings.MaxNumberOfRetries,
                RecoveryWaitTimeMilliseconds = settings.RecoveryWaitTimeMilliseconds
            };
        }
    }
}
