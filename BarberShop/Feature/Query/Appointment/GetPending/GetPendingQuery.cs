using BarberShop.DTO.ResponseResult;
using MediatR;

namespace BarberShop.Feature.Query.Appointment.GetPending
{
    public record GetPendingQuery : IRequest<ResponseDTO>
    {
    }
}
