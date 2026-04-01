using ApartmentRentals. Data.DTOs;
using ApartmentRentals.Data.Models;
using AutoMapper;

namespace ApartmentRentals.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SpaceNoIdDTO, Space>();
            CreateMap<Space, SpaceListDTO>();
            CreateMap<LandlordNoIdDTO, Landlord>();
            CreateMap<RentalContractNoIdDTO, RentalContract>();
            CreateMap<TenantNoIdDTO, Tenant>();
            CreateMap<UserNoIdDTO, User>();
            CreateMap<Space, SpaceDTO>();
            CreateMap<Landlord, LandlordDTO>();
            CreateMap<RentalContract, RentalContractDTO>();
            CreateMap<Tenant, TenantDTO>();
            CreateMap<User, UserDTO>();
        }
    }
}
