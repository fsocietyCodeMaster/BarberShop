using AutoMapper;
using BarberShop.DTO.Barber;
using BarberShop.DTO.BarberShop;
using BarberShop.Model;

namespace BarberShop.Mapping
{
    public class MappingData : Profile
    {
        public MappingData()
        {
            CreateMap<T_BarberShop, BarberShopsDTO>();
            CreateMap<T_User, BarberInfoDTO>();
        }
    }
}
