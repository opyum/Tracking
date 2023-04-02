using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Connexity.BC.Tracking.Domain.Ports
{
    using Connexity.BC.Tracking.Domain.States;

    /// <summary>
    /// Interface for the StorePort that handles the access to the DB
    /// </summary>
    public interface IStorePort
    {
        Task<IList<TrackingState>> GetAllAsync(CancellationToken cancellationToken = default);

        Task<TrackingState> GetAsync(Guid Id, CancellationToken cancellationToken = default);

        Task SaveAsync(TrackingState Tracking, CancellationToken cancellationToken = default);

        Task<TrackingState> GetByCodeAsync(string code, CancellationToken cancellationToken = default);
    }
}