//using BarberShop.DTO.ResponseResult;
//using BarberShop.Repository;
//using MediatR;

//namespace BarberShop.Feature.Command.Appointment
//{
//    public class SetAppointmentCommandHandler : IRequestHandler<SetAppointmentCommand, ResponseDTO>
//    {
//        private readonly IUser _user;

//        public SetAppointmentCommandHandler(IUser user)
//        {
//            _user = user;
//        }
//        public async Task<ResponseDTO> Handle(SetAppointmentCommand request, CancellationToken cancellationToken)
//        {
//            var result = await _user.SetAppointment(request.ID_Barber, request.AppointmentDate, request.StartTime, request.EndTime);
//            return result;
//        }
//    }
//}
