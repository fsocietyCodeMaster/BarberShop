using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using MediatR;

namespace BarberShop.Feature.Query.Client.GetBarberAppointment
{
    public class GetBarberAppointmentQueryHandler : IRequestHandler<GetBarberAppointmentQuery, ResponseDTO>
    {
        private readonly IClient _client;

        public GetBarberAppointmentQueryHandler(IClient client)
        {
            _client = client;
        }
        public Task<ResponseDTO> Handle(GetBarberAppointmentQuery request, CancellationToken cancellationToken)
        {
            var resutl = _client.GetBarberAppointment(request.id,request.date);
            return resutl;
        }
    }
}
