using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Query.BarberSchedule.GetSchedule
{
    public class GetScheduleQueryHandler : IRequestHandler<GetScheduleQuery, ResponseDTO>
    {
        private readonly IBarberSchedule _barberSchedule;

        public GetScheduleQueryHandler(IBarberSchedule barberSchedule)
        {
            _barberSchedule = barberSchedule;
        }
        public async Task<ResponseDTO> Handle(GetScheduleQuery request, CancellationToken cancellationToken)
        {
            var result = await _barberSchedule.GetSchedules();
            return result;
        }
    }
}
