
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Connexity.BC.Tracking.Service.Adapters.Spi.Events
{
    using Connexity.BC.Tracking.Domain.Events;
    using Connexity.BC.Tracking.Domain.Ports;
    using static Connexity.BC.Tracking.Domain.Entities.Tracking;
    using static Connexity.BC.Tracking.Domain.Requests.Models.Request;

    public class DaprNotifyAdapter : INotifyPort
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DaprNotifyAdapter"/> class.
        /// </summary>
        /// <param name="daprClient">The dapr client.</param>
        public DaprNotifyAdapter()
        {
        }

        /// <summary>
        /// Notifies the event asynchronous.
        /// </summary>
        /// <typeparam name="TEvent">The type of the event.</typeparam>
        /// <param name="evt">The evt.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task NotifyAsync<TEvent>(TEvent evt, CancellationToken cancellationToken = default)
            where TEvent : DomainEvent
        {
            var topicName = evt switch
            {
                EnergyTrackingChanged<DimmingProgramWeekDayEntity, DimmingProgramExceptionDayEntity> => Constants.TopicNames.EnergyTrackingChanged,
                EnergyTrackingChanged<DimmingProgramWeekDayRequest, DimmingProgramExceptionDayRequest> => Constants.TopicNames.EnergyTrackingChanged,
                EnergyTrackingChangeRequested => Constants.TopicNames.DimmingPrograms_TrackingChangeRequested,
                _ => throw new InvalidOperationException("Event is not defined.")
            };

            //await this.daprClient.PublishEventAsync(Constants.PubSubNames.PubSub1, topicName, evt, cancellationToken);
        }
    }
}