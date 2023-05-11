using CustomerManagement.Common.Abstract;
using MediatR;

namespace CustomerManagement.Domain.Customers.Features.Commands.CustomerDelete
{
    /// <summary>
    ///     Delete a customer (taking contact details and addresses with it)
    /// </summary>
    public class CustomerDeleteCommand : IRequest, ITrackableRequest
    {
        /// <summary>
        ///     Customer's unique identifier
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        ///     Correlation id for tracking the request
        /// </summary>
        public string CorrelationId { get; set; } = null!;
    }
}