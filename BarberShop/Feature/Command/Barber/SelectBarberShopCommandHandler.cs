using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Command.Barber
{
    public class SelectBarberShopCommandHandler : IRequestHandler<SelectBarberShopCommand, ResponseDTO>
    {
        private readonly IBarber _barber;

        public SelectBarberShopCommandHandler(IBarber barber)
        {
            _barber = barber;
        }
        public async Task<ResponseDTO> Handle(SelectBarberShopCommand request, CancellationToken cancellationToken)
        {
            var result = await _barber.SelectBarberShop(request.id);
            return result;
        }
    }
}
