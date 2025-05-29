using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Query.Barber.SelectBarber
{
    public class SelectBarberQueryHandler : IRequestHandler<SelectBarberQuery, ResponseDTO>
    {
        private readonly IBarber _barber;

        public SelectBarberQueryHandler(IBarber barber)
        {
            _barber = barber;
        }
        public async Task<ResponseDTO> Handle(SelectBarberQuery request, CancellationToken cancellationToken)
        {
            var result = await _barber.SelectBarber(request.id);
            return result;
        }
    }
}
