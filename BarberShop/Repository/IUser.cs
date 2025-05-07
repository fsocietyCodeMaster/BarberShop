using BarberShop.DTO.ResponseResult;
using System.Security.Claims;

namespace BarberShop.Repository
{
    public interface IUser
    {
        Task<ResponseDTO> DeleteUserAsync(Guid id);
        Task<ResponseDTO> UpdateUserAsync(string Username, string FullName, string PhoneNumber, IFormFile ImageUrl);
        Task<ResponseDTO> GetUser(ClaimsPrincipal user);
        Task<ResponseDTO> SelectBarber(string id);
    }
}
