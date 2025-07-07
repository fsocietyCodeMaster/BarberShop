using AutoMapper;
using BarberShop.DTO.Appointment;
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
            CreateMap<T_BarberShop, BarberShopDTO>();
            CreateMap<T_BarberShop, BarberShopsForGetAllDTO>();
            CreateMap<T_User, BarberInfoForBarberShopDTO>();
            CreateMap<T_Appointment, ShowAppointmentClient>();

        }
    }
}
