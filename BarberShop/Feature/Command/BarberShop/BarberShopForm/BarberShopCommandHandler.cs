using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Command.BarberShop.BarberShopForm
{
    public class BarberShopCommandHandler : IRequestHandler<BarberShopCommand, ResponseDTO>
    {
        private readonly IBarberShop _barberShop;

        public BarberShopCommandHandler(IBarberShop barberShop)
        {
            _barberShop = barberShop;
        }
        public async Task<ResponseDTO> Handle(BarberShopCommand request, CancellationToken cancellationToken)
        {
            var result = await _barberShop.ShopForm(request.Name, request.Address, request.Phone, request.Description);
            if (result != null)
            {
                return result;
            }
            else
            {
                var error = new ResponseDTO
                {
                    Message = "مشکل در ایجاد آرایشگاه",
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Data = null
                };
                return error;
            }
        }
    }
}
