using BarberShop.DTO.ResponseResult;
using MediatR;

namespace BarberShop.Feature.Query.Barber.GetAvailableTime
{
    public record BarberFreeTimeQuery(string barberId, DateTime date) : IRequest<ResponseDTO>
    {
    }
}
