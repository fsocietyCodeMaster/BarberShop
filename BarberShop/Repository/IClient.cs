using BarberShop.DTO.ResponseResult;

namespace BarberShop.Repository
{
    public interface IClient
    {
        Task<ResponseDTO> GetBarberSchedule(string barberId);
    }
}
