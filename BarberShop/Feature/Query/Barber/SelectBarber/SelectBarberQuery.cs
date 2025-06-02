using BarberShop.DTO.ResponseResult;
using MediatR;

namespace BarberShop.Feature.Query.Barber.SelectBarber
{
    public record SelectBarberQuery(string id) : IRequest<ResponseDTO>
    {
    }
}
