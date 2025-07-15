using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Query.Client.GetClientAppointment
{
    public class GetClientAppointmentQueryHandler : IRequestHandler<GetClientAppointmentQuery,ResponseDTO>
    {
        private readonly IClient _client;

        public GetClientAppointmentQueryHandler(IClient client)
        {
            _client = client;
        }
        public Task<ResponseDTO> Handle(GetClientAppointmentQuery request, CancellationToken cancellationToken)
        {
            var resutl = _client.GetClientAppointment(request.clientId);
            return resutl;
        }
    }
}
