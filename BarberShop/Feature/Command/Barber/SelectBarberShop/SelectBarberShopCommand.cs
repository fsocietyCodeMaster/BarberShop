using BarberShop.DTO.ResponseResult;
using MediatR;

namespace BarberShop.Feature.Command.Barber.SelectBarberShop
{
    public record SelectBarberShopCommand(string id) : IRequest<ResponseDTO>
    {
    }
}
