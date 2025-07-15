using BarberShop.DTO.ResponseResult;

namespace BarberShop.Repository
{
    public interface IClient
    {
        Task<ResponseDTO> GetBarberSchedule(string barberId);
        Task<ResponseDTO> GetBarberAppointment(string barberId,DateTime date);
        Task<ResponseDTO> GetClientAppointment(string clientId);
    }
}
