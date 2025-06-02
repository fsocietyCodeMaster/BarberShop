using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Query.Appointment.GetPending
{
    public class GetPendingQueryHandler : IRequestHandler<GetPendingQuery, ResponseDTO>
    {
        private readonly IAppointment _appointment;

        public GetPendingQueryHandler(IAppointment appointment)
        {
            _appointment = appointment;
        }
        public Task<ResponseDTO> Handle(GetPendingQuery request, CancellationToken cancellationToken)
        {
            var result = _appointment.GetPendingAppointments();
            return result;
        }
    }
}
