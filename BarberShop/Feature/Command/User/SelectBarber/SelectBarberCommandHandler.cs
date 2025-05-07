using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Command.User.SelectBarber
{
    public class SelectBarberCommandHandler : IRequestHandler<SelectBarberCommand, ResponseDTO>
    {
        private readonly IUser _user;

        public SelectBarberCommandHandler(IUser user)
        {
            _user = user;
        }
        public async Task<ResponseDTO> Handle(SelectBarberCommand request, CancellationToken cancellationToken)
        {
            var result = await _user.SelectBarber(request.id);
            return result;
        }
    }
}
