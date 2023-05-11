namespace CustomerManagement.Domain.Customers
{
    /// <summary>
    ///     For ensuring all requests are trackable by their correlationId
    /// </summary>
    public interface ITrackableCustomerRequest
    {
        /// <summary>
        ///     The correlation id of the request
        /// </summary>
        string CorrelationId { get; set; }
    }
}