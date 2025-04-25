using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Query.User.GetUser
{
    public class GetUserCommandHandler : IRequestHandler<GetUserCommand, ResponseDTO>
    {
        private readonly IUser _user;

        public GetUserCommandHandler(IUser user)
        {
            _user = user;
        }

        public async Task<ResponseDTO> Handle(GetUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _user.GetUser(request.user);
            return result;

        }
    }
}
