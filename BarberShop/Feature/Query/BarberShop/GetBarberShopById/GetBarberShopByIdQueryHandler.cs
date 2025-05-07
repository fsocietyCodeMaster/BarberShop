using BarberShop.DTO.ResponseResult;
using BarberShop.Feature.Query.BarberShop.GetBarberById;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Query.BarberShop.GetBarberShopById
{
    public class GetBarberShopByIdQueryHandler : IRequestHandler<GetBarberShopByIdQuery, ResponseDTO>
    {
        private readonly IBarberShop _barberShop;

        public GetBarberShopByIdQueryHandler(IBarberShop barberShop)
        {
            _barberShop = barberShop;
        }
        public async Task<ResponseDTO> Handle(GetBarberShopByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _barberShop.GetAvailableBarberShop(request.id);
            return result;
        }
    }
}
