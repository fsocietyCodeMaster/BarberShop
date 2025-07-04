using BarberShop.DTO.ResponseResult;
using MediatR;

namespace BarberShop.Feature.Query.Client.GetBarberSchedule
{
    public record GetBarberSheduleQuery(string barberId) : IRequest<ResponseDTO>
    {
        
    }
}
