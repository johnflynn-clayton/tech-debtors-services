using Cmh.Vmf.Infrastructure.Common.Extensions;
using CMH.Common.Events.Models;
using CMH.Common.RabbitMQClient;
using CMH.MobileHomeTracker.Domain.Events;
using CMH.MobileHomeTracker.Dto.Events;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace CMH.MobileHomeTracker.Events
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IEventProducer _eventProducer;
        private readonly ILogger _logger;
        private readonly RabbitMqSettings _settings;

        public EventPublisher(IEventProducer eventProducer, ILogger<EventPublisher> logger, IOptions<RabbitMqSettings> settings)
        {
            _eventProducer = eventProducer;
            _logger = logger;
            _settings = settings.Value;
        }

        public void PublishSampleCreatedEvent(Guid id)
        {
            var message = CreateEvent(SampleEvent.EventType, SampleCreatedEvent.EventSubType, new SampleCreatedEvent { SampleId = id });

            _logger.LogInformation($"Raising RabbitMq event: {message.ToJson()}");
            _eventProducer.PublishMessage(message);
        }

        public void PublishSampleDeletedEvent(Guid id)
        {
            var message = CreateEvent(SampleEvent.EventType, SampleDeletedEvent.EventSubType, new SampleDeletedEvent { SampleId = id });

            _logger.LogInformation($"Raising RabbitMq event: {message.ToJson()}");
            _eventProducer.PublishMessage(message);
        }

        public void PublishSampleUpdatedEvent(Guid id)
        {
            var message = CreateEvent(SampleEvent.EventType, SampleUpdatedEvent.EventSubType, new SampleUpdatedEvent { SampleId = id });

            _logger.LogInformation($"Raising RabbitMq event: {message.ToJson()}");
            _eventProducer.PublishMessage(message);
        }

        private EMBEvent<T> CreateEvent<T>(string eventType, string eventSubType, T payload)
        {
            return new EMBEvent<T>
            {
                CorrelationID = Guid.NewGuid().ToString(),
                EventClass = EventClass.Detail,
                EventVersion = _settings.EventVersion,
                Exchange = _settings.ExchangeName,
                RoutingKey = string.Empty,
                Source = _settings.EventSource,
                EventSubType = eventSubType,
                EventType = eventType,
                Payload = payload
            };
        }
    }
}
