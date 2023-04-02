using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Connexity.BC.Tracking.Domain.Tests.Helpers
{
    using Connexity.BC.Tracking.Domain.Ports;
    using Connexity.BC.Tracking.Domain.States;

    /// <summary>
    /// The in memory store port.
    /// </summary>
    public class InMemoryStorePort : IStorePort
    {
        /// <summary>
        /// The states
        /// </summary>
        private readonly IDictionary<Guid, TrackingState> states;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryStorePort"/> class.
        /// </summary>
        /// <param name="items">The items.</param>
        public InMemoryStorePort(IEnumerable<TrackingState> items)
        {
            this.states = items != null ? items.ToDictionary(x => x.Id) : new Dictionary<Guid, TrackingState>();
        }

        /// <summary>
        /// Gets all asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<IList<TrackingState>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult((IList<TrackingState>)this.states.Values.ToList());
        }

        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <param name="Id">The Tracking identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        public Task<TrackingState> GetAsync(Guid Id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(this.states[Id]);
        }

        public Task<TrackingState> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(this.states.Values.FirstOrDefault(x => x.Code == code));
        }

        /// <summary>
        /// Saves the asynchronous.
        /// </summary>
        /// <param name="Tracking">The Tracking.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async Task SaveAsync(TrackingState Tracking, CancellationToken cancellationToken = default)
        {
            await Task.Run(() => this.states[Tracking.Id] = Tracking);
        }
    }
}