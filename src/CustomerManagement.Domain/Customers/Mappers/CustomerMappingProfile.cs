using AutoMapper;
using CustomerManagement.Data.Models;
using CustomerManagement.Domain.Customers.Features.Commands.AddAddressToCustomer;
using CustomerManagement.Domain.Customers.Features.Commands.CustomerCreate;
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
        ///     ctor with mapping rules - Kept the models throughout similar to greatly assist with automapper usage. A real app
        ///     probably wouldn't have models mirroring quite this closely
        /// </summary>
        public CustomerMappingProfile()
        {
            DbModelsToResponses();
            DtoToQueries();
            DtoToCommands();
        }

        private void DtoToCommands()
        {
            CreateMap<CustomerCreateCommandDto, CustomerCreateCommand>();
            CreateMap<CustomerCreateCommand, Customer>();

            CreateMap<AddAddressToCustomerDto, AddAddressToCustomerCommand>();
            CreateMap<AddAddressToCustomerCommand, Address>();
        }

        private void DtoToQueries()
        {
            CreateMap<CustomersByPagesQueryDto, CustomerByPagesQuery>();
        }

        private void DbModelsToResponses()
        {
            CreateMap<Customer, CustomerWithAllDetailsResponseDto>();
            CreateMap<ContactDetail, ContactDetailsResponseDto>();

            CreateMap<Address, AddressResponseDto>()
                .ForMember(dest => dest.IsDefault,
                    opts => opts.MapFrom(src => src.CustomerAddresses.First().IsDefaultAddress));
        }
    }
}