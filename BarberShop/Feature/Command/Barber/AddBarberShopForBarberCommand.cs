using BarberShop.DTO.ResponseResult;
using MediatR;

namespace BarberShop.Feature.Command.Barber
{
    public record AddBarberShopForBarberCommand(Guid id) : IRequest<ResponseDTO>
    {
    }
}
