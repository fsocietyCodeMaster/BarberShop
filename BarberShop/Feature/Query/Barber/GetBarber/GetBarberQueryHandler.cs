//using BarberShop.DTO.ResponseResult;
//using BarberShop.Repository;
//using MediatR;

//namespace BarberShop.Feature.Query.Barber.GetBarber
//{
//    public class GetBarberQueryHandler : IRequestHandler<GetBarberQuery, ResponseDTO>
//    {
//        private readonly IBarber _barber;

//        public GetBarberQueryHandler(IBarber barber)
//        {
//            _barber = barber;
//        }
//        public async Task<ResponseDTO> Handle(GetBarberQuery request, CancellationToken cancellationToken)
//        {
//            var result = await _barber.GetBarber(request.id);
//            return result;
//        }
//    }
//}
