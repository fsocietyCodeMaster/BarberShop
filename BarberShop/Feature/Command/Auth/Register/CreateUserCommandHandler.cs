using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Command.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseDTO>
    {
        private readonly IAuthentication _user;

        public CreateUserCommandHandler(IAuthentication user)
        {
            _user = user;
        }
        public async Task<ResponseDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _user.RegisterAsync(request.UserName,request.Password,request.FullName,request.Bio,request.PhoneNumber,request.Role);
            return result;

        }
    }
}
