using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Command.User.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ResponseDTO>
    {
        private readonly IUser _user;

        public UpdateUserCommandHandler(IUser user)
        {
            _user = user;
        }
        public async Task<ResponseDTO> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await _user.UpdateUserAsync(request.UserName,request.FullName,request.PhoneNumber,request.ImageUrl,request.Bio);
            return result;
        }
    }
}
