using BarberShop.DTO.ResponseResult;

namespace BarberShop.Repository
{
    public interface IBarber
    {
        Task<ResponseDTO> SelectBarberShop(Guid id);
        Task<ResponseDTO> UpdateBarber(string bio);
        //Task<ResponseDTO> GetBarber(string id);
        Task<ResponseDTO> SelectBarber(string id);
        Task<ResponseDTO> SetAppointment(string barberIds, DateTime appointmentTime, TimeSpan start, TimeSpan end);
        Task<ResponseDTO> GetAvailableTime(string id, DateTime date);

    }
}
