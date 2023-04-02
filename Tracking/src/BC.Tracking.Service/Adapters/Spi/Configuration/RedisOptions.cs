namespace Connexity.BC.Tracking.Service.Adapters.Spi.Configuration
{
    public class RedisOptions
    {
        public string Endpoint { get; set; }

        /// <summary>
        /// Gets or sets the key prefix.
        /// </summary>
        /// <value>
        /// The key prefix.
        /// </value>
        public string KeyPrefix { get; set; }
    }
}