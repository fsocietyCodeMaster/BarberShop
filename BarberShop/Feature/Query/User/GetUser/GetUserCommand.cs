using BarberShop.DTO.ResponseResult;
using MediatR;
using System.Security.Claims;

namespace BarberShop.Feature.Query.User.GetUser
{
    public record GetUserCommand(ClaimsPrincipal user) : IRequest<ResponseDTO>
    {
    }
}
