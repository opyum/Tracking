using System.Threading;
using System.Threading.Tasks;

namespace Connexity.BC.Tracking.Domain.Ports
{
    using Connexity.BC.Tracking.Domain.Events;

    /// <summary>
    /// Interface for the NotifyPort that publishes on MessageBus
    /// </summary>
    public interface INotifyPort
    {
        Task NotifyAsync<TEvent>(TEvent evt, CancellationToken cancellationToken = default)
            where TEvent : DomainEvent;
    }
}