using System.Text;
using CustomerManagement.Data.Models;
using CustomerManagement.Domain.Abstract;
using Microsoft.IdentityModel.Tokens;

namespace CustomerManagement.Domain.Customers
{
    /// <summary>
    ///     Helper extensions for customer objects
    /// </summary>
    public static class CustomerExtensions
    {
        /// <summary>
        ///     Get customer initials from a customer object - useful for logging whilst redacting PII
        /// </summary>
        /// <param name="customerRequest"></param>
        /// <returns></returns>
        public static string CustomerInitials(this ICustomerRequest customerRequest)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(InitialsFromString(customerRequest.FirstName));
            stringBuilder.Append(InitialsFromString(customerRequest.Middlenames));
            stringBuilder.Append(InitialsFromString(customerRequest.Surname));

            return stringBuilder.ToString();
        }

        /// <summary>
        ///     Split a string into words and return the joined initials
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string InitialsFromString(string? str)
        {
            if (str.IsNullOrEmpty())
                return string.Empty;

            var initials = str!.Split()
                .Where(word => !word.IsNullOrEmpty())
                .Select(word => word.ToUpper()[0]);

            return string.Join("", initials);
        }

        /// <summary>
        ///     Create a default / initial contact detail from a customer request
        /// </summary>
        /// <param name="customerRequestCreateCommand"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static ContactDetail CreateInitialCustomerContactDetail(this ICustomerRequest customerRequestCreateCommand,
            Guid customerId)
        {
            return new ContactDetail
            {
                Id = Guid.NewGuid(),
                CustomerId = customerId,
                Detail = customerRequestCreateCommand.EMail,
                ContactType = ContactDetailsType.Email
            };
        }
    }
}