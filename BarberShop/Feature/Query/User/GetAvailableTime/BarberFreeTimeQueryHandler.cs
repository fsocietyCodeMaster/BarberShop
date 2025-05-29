using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Query.User.GetAvailableTime
{
    public class BarberFreeTimeQueryHandler : IRequestHandler<BarberFreeTimeQuery, ResponseDTO>
    {
        private readonly IUser _user;

        public BarberFreeTimeQueryHandler(IUser user)
        {
            _user = user;
        }
        public async Task<ResponseDTO> Handle(BarberFreeTimeQuery request, CancellationToken cancellationToken)
        {
            var result = await _user.GetAvailableTime(request.barberId, request.date);
            return result;
        }
    }
}
