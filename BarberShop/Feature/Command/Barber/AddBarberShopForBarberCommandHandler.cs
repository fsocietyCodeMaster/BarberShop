using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Command.Barber
{
    public class AddBarberShopForBarberCommandHandler : IRequestHandler<AddBarberShopForBarberCommand, ResponseDTO>
    {
        private readonly IBarber _barber;

        public AddBarberShopForBarberCommandHandler(IBarber barber)
        {
            _barber = barber;
        }
        public Task<ResponseDTO> Handle(AddBarberShopForBarberCommand request, CancellationToken cancellationToken)
        {
            var result = _barber.SelectBarberShop(request.id);
            return result;
        }
    }
}
