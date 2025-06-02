using BarberShop.DTO.ResponseResult;
using MediatR;

namespace BarberShop.Feature.Query.BarberSchedule.GetAvailableTime
{
    public record BarberFreeTimeQuery(string barberId, DateTime date) : IRequest<ResponseDTO>
    {
    }
}
