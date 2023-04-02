namespace Connexity.BC.Tracking.Domain.Ports
{
    /// <summary>
    /// Interface for a ServiceConfigPort
    /// </summary>
    public interface IServiceConfigPort
    {
        /// <summary>
        /// Gets the specified key.
        /// </summary>
        T Get<T>(string key);
    }
}