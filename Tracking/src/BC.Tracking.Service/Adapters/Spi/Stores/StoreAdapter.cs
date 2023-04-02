using Connexity.BC.Tracking.Domain.Exceptions;
using Connexity.BC.Tracking.Domain.Ports;
using Connexity.BC.Tracking.Domain.States;
using Connexity.BC.Tracking.Service.Adapters.Spi.Configuration;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Connexity.BC.Tracking.Service.Adapters.Spi.Store
{
    public class RedisStoreAdapter : IStorePort
    {
        private readonly RedisOptions redisOptions;
        private readonly Lazy<ConnectionMultiplexer> connectionMultiplexer;

        public RedisStoreAdapter(IOptions<RedisOptions> redisOptions)
        {
            this.redisOptions = redisOptions.Value;
            this.connectionMultiplexer = new Lazy<ConnectionMultiplexer>(() =>
                ConnectionMultiplexer.Connect(this.redisOptions.Endpoint));
        }

        private IDatabase GetDatabase()
        {
            return this.connectionMultiplexer.Value.GetDatabase();
        }

        private string GetKeyPrefix()
        {
            return $"{this.redisOptions.KeyPrefix}||";
        }

        public async Task<IList<TrackingState>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var database = this.GetDatabase();
            var server = this.connectionMultiplexer.Value.GetServer(this.redisOptions.Endpoint);
            var keys = server.Keys(pattern: $"{this.GetKeyPrefix()}*").Select(rv =>
            {
                var key = rv.ToString();
                return key.Substring(key.LastIndexOf("||") + 2);
            }).ToList();

            if (keys.Count == 0)
            {
                throw new NoTrackingsFoundException("No dimming calendars found");
            }

            var items = await database.StringGetAsync(keys.Select(k => (RedisKey)k).ToArray());

            return items
                .Select(i => JsonSerializer.Deserialize<TrackingState>(i, new JsonSerializerOptions()))
                .ToList();
        }

        public async Task<TrackingState> GetAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var database = this.GetDatabase();
            var key = $"{this.GetKeyPrefix()}{id}";
            var item = await database.StringGetAsync(key);

            if (item.IsNull)
            {
                return null;
            }

            return JsonSerializer.Deserialize<TrackingState>(item, new JsonSerializerOptions());
        }

        public async Task SaveAsync(TrackingState Tracking, CancellationToken cancellationToken = default)
        {
            var database = this.GetDatabase();
            var key = $"{this.GetKeyPrefix()}{Tracking.Id}";
            var value = JsonSerializer.Serialize(Tracking);

            await database.StringSetAsync(key, value);
        }

        public async Task<TrackingState> GetByCodeAsync(string code, CancellationToken cancellationToken = default)
        {
            var database = this.GetDatabase();
            var server = this.connectionMultiplexer.Value.GetServer(this.redisOptions.Endpoint);
            var keys = server.Keys(pattern: $"{this.GetKeyPrefix()}*").Select(rv =>
            {
                var key = rv.ToString();
                return key.Substring(key.LastIndexOf("||") + 2);
            }).ToList();

            if (keys.Count == 0)
            {
                return null;
            }

            var items = await database.StringGetAsync(keys.Select(k => (RedisKey)k).ToArray());

            return items
                .Select(i => JsonSerializer.Deserialize<TrackingState>(i, new JsonSerializerOptions()))
                .FirstOrDefault(x => x.Code == code);
        }
    }
}
