using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Command.BarberSchedule.CreateSchedule
{
    public class CreateBarberScheduleCommandHandler : IRequestHandler<CreateBarberScheduleCommand, ResponseDTO>
    {
        private readonly IBarberSchedule _barberSchedule;

        public CreateBarberScheduleCommandHandler(IBarberSchedule barberSchedule)
        {
            _barberSchedule = barberSchedule;
        }
        public async Task<ResponseDTO> Handle(CreateBarberScheduleCommand request, CancellationToken cancellationToken)
        {
            var result = await _barberSchedule.CreateSchedule(request.StartTimeMorning, request.EndTimeMorning, request.StartTimeEvening, request.EndTimeEvening, request.ScopeTime, request.SaturdayWork, request.SundayWork, request.MondayWork, request.TuesdayWork, request.WednesdayWork, request.ThursdayWork, request.FridayWork);
            return result;
        }
    }
}
