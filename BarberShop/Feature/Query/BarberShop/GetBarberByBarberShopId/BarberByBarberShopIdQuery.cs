using BarberShop.DTO.ResponseResult;
using MediatR;

namespace BarberShop.Feature.Query.BarberShop.GetBarberByBarberShopId
{
    public record BarberByBarberShopIdQuery() : IRequest<ResponseDTO>
    {
    }
}
