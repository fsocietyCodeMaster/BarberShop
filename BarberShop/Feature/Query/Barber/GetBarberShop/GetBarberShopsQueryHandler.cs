using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Query.Barber.GetBarberShop
{
    public class GetBarberShopsQueryHandler : IRequestHandler<GetBarberShopsQuery, ResponseDTO>
    {
        private readonly IBarber _barber;

        public GetBarberShopsQueryHandler(IBarber barber)
        {
            _barber = barber;
        }
        public async Task<ResponseDTO> Handle(GetBarberShopsQuery request, CancellationToken cancellationToken)
        {
            var result = await _barber.GetAllBarberShopForBarbers();
            return result;
        }
    }
}
