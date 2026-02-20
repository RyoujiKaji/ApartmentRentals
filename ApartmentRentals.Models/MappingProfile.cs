using ApartmentRentals.Main.DTOs;
using ApartmentRentals.Main.Models;
using AutoMapper;

namespace ApartmentRentals.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SpaceCreateDTO, Space>();
            CreateMap<Space, SpaceListDTO>();
            CreateMap<LandlordDTO, Landlord>();
            CreateMap<RentalContractDTO, RentalContract>();
            CreateMap<TenantDTO, Tenant>();
            CreateMap<UserDTO, User>();
        }
    }
}
