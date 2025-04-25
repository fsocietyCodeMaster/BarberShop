using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Command.Auth.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ResponseDTO>
    {
        private readonly IAuthentication _user;

        public LoginCommandHandler(IAuthentication user)
        {
            _user = user;
        }
        public async Task<ResponseDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var result = await _user.LoginAsync(request.Username, request.Password);
            return result;
        }
    }
}
