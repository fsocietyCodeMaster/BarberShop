using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Command.Appointment.SetAppointment
{
    public class SetAppointmentCommandHandler : IRequestHandler<SetAppointmentCommand, ResponseDTO>
    {
        private readonly IAppointment _appointment;

        public SetAppointmentCommandHandler(IAppointment appointment)
        {
            _appointment = appointment;
        }
        public async Task<ResponseDTO> Handle(SetAppointmentCommand request, CancellationToken cancellationToken)
        {
            var result = await _appointment.SetAppointment(request.ID_Barber, request.AppointmentDate, request.StartTime, request.EndTime);
            return result;
        }
    }
}
