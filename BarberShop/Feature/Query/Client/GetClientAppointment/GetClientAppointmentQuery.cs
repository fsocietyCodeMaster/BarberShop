using BarberShop.DTO.ResponseResult;
using MediatR;

namespace BarberShop.Feature.Query.Client.GetClientAppointment
{
    public record GetClientAppointmentQuery(string clientId) : IRequest<ResponseDTO>
    {
    }
}
