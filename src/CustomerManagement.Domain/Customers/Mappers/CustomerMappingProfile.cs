using AutoMapper;
using CustomerManagement.Data.Models;
using CustomerManagement.Domain.Customers.Features.Queries.CustomerByPages;
using CustomerManagement.Domain.Customers.Responses;

namespace CustomerManagement.Domain.Customers.Mappers
{
    /// <summary>
    ///     Basic mappers for customer objects
    /// </summary>
    public class CustomerMappingProfile : Profile
    {
        /// <summary>
        ///     ctor with mapping rules
        /// </summary>
        public CustomerMappingProfile()
        {
            CreateMap<Customer, CustomerWithAllDetailsResponseDto>();
            CreateMap<Address, AddressResponseDto>();
            CreateMap<ContactDetail, ContactDetailsResponseDto>();

            CreateMap<CustomersByPagesQueryDto, ByPagesQuery>();
        }
    }
}