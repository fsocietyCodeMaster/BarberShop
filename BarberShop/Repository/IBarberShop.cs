using BarberShop.DTO.ResponseResult;

namespace BarberShop.Repository
{
    public interface IBarberShop
    {
        Task<ResponseDTO> ShopForm(string Name, string Address, string Phone, string Description, IFormFile ImageUrl);
        Task<ResponseDTO> GetAllAvailableBarberShops();
        Task<ResponseDTO> GetAvailableBarberShop(Guid id);
        Task<ResponseDTO> UpdateBarberShopForm(Guid id,string Name,string Address, string Phone, string Description,IFormFile ImageUrl);
        Task<ResponseDTO> GetPendingBarbers();
        Task<ResponseDTO> ApproveUser(string UserId, string Approve,Guid? barberShopId);

    }
}
