using BarberShop.DTO.ResponseResult;
using MediatR;

namespace BarberShop.Feature.Command.BarberShop.Approval
{
    public record ApprovalCommand(string userId, string approval,Guid barberShopId) : IRequest<ResponseDTO>
    {
    }
}
