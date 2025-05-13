using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Command.BarberShop.UpdateBarberShopForm
{
    public class UpdateBarberShopCommandHandler : IRequestHandler<UpdateBarberShopCommand, ResponseDTO>
    {
        private readonly IBarberShop _barberShop;

        public UpdateBarberShopCommandHandler(IBarberShop barberShop)
        {
            _barberShop = barberShop;
        }
        public Task<ResponseDTO> Handle(UpdateBarberShopCommand request, CancellationToken cancellationToken)
        {
            var result = _barberShop.UpdateBarberShopForm(request.id,request.Name, request.Address, request.Phone, request.Description,request.ImageUrl);
            return result;
        }
    }
}
