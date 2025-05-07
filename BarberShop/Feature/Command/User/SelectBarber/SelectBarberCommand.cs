using BarberShop.DTO.ResponseResult;
using MediatR;

namespace BarberShop.Feature.Command.User.SelectBarber
{
    public record SelectBarberCommand(string id) : IRequest<ResponseDTO>
    {
    }
}
