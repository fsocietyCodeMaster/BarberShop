using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Command.Appointment
{
    public class SetAppointmentCommandHandler : IRequestHandler<SetAppointmentCommand, ResponseDTO>
    {
        private readonly IBarber _barber;

        public SetAppointmentCommandHandler(IBarber barber)
        {
            _barber = barber;
        }
        public async Task<ResponseDTO> Handle(SetAppointmentCommand request, CancellationToken cancellationToken)
        {
            var result = await _barber.SetAppointment(request.ID_Barber, request.AppointmentDate, request.StartTime, request.EndTime);
            return result;
        }
    }
}
