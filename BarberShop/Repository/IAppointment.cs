using BarberShop.DTO.ResponseResult;

namespace BarberShop.Repository
{
    public interface IAppointment
    {
        Task<ResponseDTO> GetPendingAppointments();
        Task<ResponseDTO> SetAppointment(string barberIds, DateTime appointmentTime, TimeSpan start, TimeSpan end);

    }
}
