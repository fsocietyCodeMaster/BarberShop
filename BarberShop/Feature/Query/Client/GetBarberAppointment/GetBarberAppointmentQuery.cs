using BarberShop.DTO.ResponseResult;
using MediatR;

namespace BarberShop.Feature.Query.Client.GetBarberAppointment
{
    public record GetBarberAppointmentQuery(string id,DateTime date) : IRequest<ResponseDTO>
    {
    }
}
