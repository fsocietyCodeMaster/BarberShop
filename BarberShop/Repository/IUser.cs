using BarberShop.DTO.ResponseResult;
using System.Security.Claims;

namespace BarberShop.Repository
{
    public interface IUser
    {
        Task<ResponseDTO> DeleteUserAsync(Guid id);
        Task<ResponseDTO> UpdateUserAsync(string Username, string FullName, string PhoneNumber, IFormFile ImageUrl,string? bio);
        Task<ResponseDTO> GetUser(ClaimsPrincipal user);
        Task<ResponseDTO> SelectBarber(string id);
        //Task<ResponseDTO> SetAppointment(string barberIds,DateTime appointmentTime,TimeSpan start, TimeSpan end);
        Task<ResponseDTO> GetAvailableTime(string id, DateTime date);

    }
}
