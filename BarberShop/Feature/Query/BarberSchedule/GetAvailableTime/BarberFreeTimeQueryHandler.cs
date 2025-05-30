using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Query.BarberSchedule.GetAvailableTime
{
    public class BarberFreeTimeQueryHandler : IRequestHandler<BarberFreeTimeQuery, ResponseDTO>
    {
        private readonly IBarberSchedule _barberSchedule;

        public BarberFreeTimeQueryHandler(IBarberSchedule barberSchedule)
        {
            _barberSchedule = barberSchedule;
        }
        public async Task<ResponseDTO> Handle(BarberFreeTimeQuery request, CancellationToken cancellationToken)
        {
            var result = await _barberSchedule.GetAvailableTime(request.barberId, request.date);
            return result;
        }
    }
}
