using BarberShop.DTO.ResponseResult;

namespace BarberShop.Repository
{
    public interface IAuthentication
    {
        Task<ResponseDTO> RegisterAsync(string UserName, string Password, string FullName, string? Bio, TimeSpan? StartTime, TimeSpan? EndTime, string PhoneNumber,string role);
        Task<ResponseDTO> LoginAsync(string UserName, string Password);
        ResponseDTO GetRoles();

    }
}
