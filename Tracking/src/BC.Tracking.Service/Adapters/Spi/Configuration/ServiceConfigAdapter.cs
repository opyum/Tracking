using Microsoft.Extensions.Configuration;

namespace Connexity.BC.Tracking.Service.Adapters.Spi.Configuration
{
    using Connexity.BC.Tracking.Domain.Ports;

    public class ServiceConfigAdapter : IServiceConfigPort
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration configuration;

        public ServiceConfigAdapter(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public T Get<T>(string key)
        {
            return this.configuration.GetSection(key).Get<T>();
        }
    }
}