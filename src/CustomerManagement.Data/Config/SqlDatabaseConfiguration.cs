namespace CustomerManagement.Data.Config
{
    /// <summary>
    ///     Configuration for database
    /// </summary>
    public sealed class SqlDatabaseConfiguration
    {
        /// <summary>
        ///     Connection string to database
        /// </summary>
        public string ConnectionString { get; set; } = null!;
    }
}