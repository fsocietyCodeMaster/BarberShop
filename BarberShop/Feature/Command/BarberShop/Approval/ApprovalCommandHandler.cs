using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Command.BarberShop.Approval
{
    public class ApprovalCommandHandler : IRequestHandler<ApprovalCommand, ResponseDTO>
    {
        private readonly IBarberShop _barberShop;

        public ApprovalCommandHandler(IBarberShop barberShop)
        {
            _barberShop = barberShop;
        }
        public async Task<ResponseDTO> Handle(ApprovalCommand request, CancellationToken cancellationToken)
        {
            var result = await _barberShop.ApproveUser(request.userId, request.approval);
            return result;
        }
    }
}
