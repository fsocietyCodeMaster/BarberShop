using BarberShop.DTO.ResponseResult;
using MediatR;

namespace BarberShop.Feature.Query.BarberShop.GetBarberById
{
    public record GetBarberShopByIdQuery(Guid id) : IRequest<ResponseDTO>
    {
    }
}
