using BarberShop.DTO.ResponseResult;
using MediatR;

namespace BarberShop.Feature.Query.User.GetAvailableTime
{
    public record BarberFreeTimeQuery(string barberId, DateTime date) : IRequest<ResponseDTO>
    {
    }
}
