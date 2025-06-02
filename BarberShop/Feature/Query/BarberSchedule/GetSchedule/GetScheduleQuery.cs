using BarberShop.DTO.ResponseResult;
using MediatR;

namespace BarberShop.Feature.Query.BarberSchedule.GetSchedule
{
    public class GetScheduleQuery() : IRequest<ResponseDTO>
    {
    }
}
