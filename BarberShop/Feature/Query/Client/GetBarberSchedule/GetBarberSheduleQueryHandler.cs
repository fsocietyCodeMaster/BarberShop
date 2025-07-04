using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Query.Client.GetBarberSchedule
{
    public class GetBarberSheduleQueryHandler : IRequestHandler<GetBarberSheduleQuery, ResponseDTO>
    {
        private readonly IClient _client;

        public GetBarberSheduleQueryHandler(IClient client)
        {
            _client = client;
        }
        public Task<ResponseDTO> Handle(GetBarberSheduleQuery request, CancellationToken cancellationToken)
        {
            var result = _client.GetBarberSchedule(request.barberId);
            return result;
        }
    }
}
