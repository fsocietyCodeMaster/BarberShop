using AutoMapper;
using BarberShop.DTO.Barber;
using BarberShop.Model;

namespace BarberShop.Mapping
{
    public class MappingData : Profile
    {
        public MappingData()
        {
            CreateMap<T_BarberShop, GetAllBarberShopsDTO>();
        }
    }
}
