namespace Connexity.BC.Tracking.Service.Adapters.Spi.Configuration
{
    public class ServiceConfigOptions
    {
        /// <summary>
        /// Gets or sets the name of the secret store.
        /// </summary>
        /// <value>
        /// The name of the secret store.
        /// </value>
        public string SecretStoreName { get; set; }

        /// <summary>
        /// Gets or sets the secret names.
        /// </summary>
        /// <value>
        /// The secret names.
        /// </value>
        public string[] SecretNames { get; set; }
    }
}