using BarberShop.DTO.ResponseResult;
using MediatR;

namespace BarberShop.Feature.Command.User.UpdateUser
{
    public class UpdateUserCommand : IRequest<ResponseDTO>
    {
        public string FullName { get; set; }
        public required string UserName { get; set; }
        public string? PhoneNumber { get; set; }
        public IFormFile? ImageUrl { get; set; }
        public string? Bio {  get; set; }
    }
}
