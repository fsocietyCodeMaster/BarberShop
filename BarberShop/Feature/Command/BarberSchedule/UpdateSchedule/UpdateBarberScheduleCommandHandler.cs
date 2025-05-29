using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Command.BarberSchedule.UpdateSchedule
{
    public class UpdateBarberScheduleCommandHandler : IRequestHandler<UpdateBarberScheduleCommand, ResponseDTO>
    {
        private readonly IBarberSchedule _barberSchedule;

        public UpdateBarberScheduleCommandHandler(IBarberSchedule barberSchedule)
        {
            _barberSchedule = barberSchedule;
        }
        public async Task<ResponseDTO> Handle(UpdateBarberScheduleCommand request, CancellationToken cancellationToken)
        {
            var result = await _barberSchedule.UpdateSchedule(request.StartTimeMorning, request.EndTimeMorning, request.StartTimeEvening, request.EndTimeEvening, request.ScopeTime, request.SaturdayWork, request.SundayWork, request.MondayWork, request.TuesdayWork, request.WednesdayWork, request.ThursdayWork, request.FridayWork);
            return result;
        }
    }
}
