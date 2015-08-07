
namespace Application.Common.Logging
{
    /// <summary>
    /// Base contract for Log abstract factory
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// Create a new ILog
        /// </summary>
        /// <returns>The ILog created</returns>
        ILogger Create();
    }
}
