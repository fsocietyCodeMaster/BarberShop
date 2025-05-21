using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Query.BarberShop.GetBarberByBarberShopId
{
    public class BarberByBarberShopIdQueryHandler : IRequestHandler<BarberByBarberShopIdQuery, ResponseDTO>
    {
        private readonly IBarberShop _barberShop;

        public BarberByBarberShopIdQueryHandler(IBarberShop barberShop)
        {
            _barberShop = barberShop;
        }
        public async Task<ResponseDTO> Handle(BarberByBarberShopIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _barberShop.GetPendingBarbers();
            return result;
        }
    }
}
