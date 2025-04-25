using AutoMapper;
using BarberShop.Context;
using BarberShop.DTO.Barber;
using BarberShop.DTO.ResponseResult;
using BarberShop.Repository;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.UnitOfWork.Barber
{
    public class BarberService : IBarber
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;

        public BarberService(BarberShopDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ResponseDTO> GetAllBarberShopForBarbers()
        {
            var barberShop = await _context.T_BarberShops.Where(c => c.IsActive).ToListAsync();
            if(barberShop != null)
            {
                var finalResult = _mapper.Map<IEnumerable<GetAllBarberShopsDTO>>(barberShop);
                var success = new ResponseDTO
                {
                    Message = "لیست آرایشگاه ها با موفقیت دریافت شد",
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Data = finalResult
                };
                return success;
            }
            else
            {
                var error = new ResponseDTO
                {
                    Message = "هیج  آرایشگاهی دریافت نشد",
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Data = null
                };
                return error;
            }
        }
    }
}