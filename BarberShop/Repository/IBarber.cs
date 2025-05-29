using BarberShop.DTO.ResponseResult;

namespace BarberShop.Repository
{
    public interface IBarber
    {
        Task<ResponseDTO> SelectBarberShop(Guid id);
        Task<ResponseDTO> UpdateBarber(string bio);
        //Task<ResponseDTO> GetBarber(string id);
    }
}
