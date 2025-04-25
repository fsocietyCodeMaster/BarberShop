using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Command.User.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ResponseDTO>
    {
        private readonly IUser _user;
        public DeleteUserCommandHandler(IUser user)
        {
            _user = user;
        }
        public async Task<ResponseDTO> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _user.DeleteUserAsync(request.userId);
            return result;
        }
    }
}
