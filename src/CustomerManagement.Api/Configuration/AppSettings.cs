using CustomerManagement.Data.Config;

namespace CustomerManagement.Api.Configuration
{
    /// <summary>
    ///     Holds general configuration for app and services
    /// </summary>
    public class AppSettings
    {
        /// <summary>
        ///     Database config
        /// </summary>
        public SqlDatabaseConfiguration Database { get; set; } = null!;
    }
}