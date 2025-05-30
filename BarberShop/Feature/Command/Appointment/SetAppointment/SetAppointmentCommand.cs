using BarberShop.DTO.ResponseResult;
using MediatR;

namespace BarberShop.Feature.Command.Appointment.SetAppointment
{
    public record SetAppointmentCommand : IRequest<ResponseDTO>
    {
        public string ID_Barber { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
