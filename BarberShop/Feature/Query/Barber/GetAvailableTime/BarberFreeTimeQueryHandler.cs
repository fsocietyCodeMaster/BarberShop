using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Query.Barber.GetAvailableTime
{
    public class BarberFreeTimeQueryHandler : IRequestHandler<BarberFreeTimeQuery, ResponseDTO>
    {
        private readonly IBarber _barber;

        public BarberFreeTimeQueryHandler(IBarber barber)
        {
            _barber = barber;
        }
        public async Task<ResponseDTO> Handle(BarberFreeTimeQuery request, CancellationToken cancellationToken)
        {
            var result = await _barber.GetAvailableTime(request.barberId, request.date);
            return result;
        }
    }
}
