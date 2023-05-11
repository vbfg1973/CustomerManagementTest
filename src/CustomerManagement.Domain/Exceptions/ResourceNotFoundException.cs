namespace CustomerManagement.Domain.Exceptions
{
    /// <summary>
    ///     For generating 404s
    /// </summary>
    public class ResourceNotFoundException : Exception
    {
        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="message"></param>
        public ResourceNotFoundException(string message) : base(message)
        {
        }
    }
}