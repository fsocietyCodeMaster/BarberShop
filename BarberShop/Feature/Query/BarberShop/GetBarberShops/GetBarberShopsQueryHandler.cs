using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Query.Barber.GetBarberShop
{
    public class GetBarberShopsQueryHandler : IRequestHandler<GetBarberShopsQuery, ResponseDTO>
    {
        private readonly IBarberShop _barberShop;

        public GetBarberShopsQueryHandler(IBarberShop barberShop)
        {
            _barberShop = barberShop;
        }
        public async Task<ResponseDTO> Handle(GetBarberShopsQuery request, CancellationToken cancellationToken)
        {
            var result = await _barberShop.GetAllAvailableBarberShops();
            return result;
        }
    }
}
