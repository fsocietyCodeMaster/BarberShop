using AutoMapper;
using BarberShop.Context;
using BarberShop.DTO.Barber;
using BarberShop.DTO.BarberShop;
using BarberShop.DTO.ResponseResult;
using BarberShop.Model;
using BarberShop.Repository;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BarberShop.UnitOfWork.BarberShop
{
    public class BarberShopService : IBarberShop
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public BarberShopService(BarberShopDbContext context,IMapper mapper, IHttpContextAccessor httpContext)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContext;
        }
        public async Task<ResponseDTO> ShopForm(string Name, string Address, string Phone, string Description)
        {
            var httpContext =  _httpContext;
            var userId = string.Empty;
            if (httpContext.HttpContext.User.Identity.IsAuthenticated)
            {
                 userId = httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
            else
            {
                var error = new ResponseDTO
                {
                    Message = "کاربر یافت نشد",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status200OK,
                    Data = null
                };
                return error;
            }
            var barberShop = new T_BarberShop
            {
                Name = Name,
                Address = Address,
                Phone = Phone,
                Description = Description,
                IsActive = true,
                CreatedAt = DateTime.Now,
                OwnerId = userId
            };
            _context.Add(barberShop);
            await _context.SaveChangesAsync();
            var success = new ResponseDTO
            {
                Message = "آرایشگاه با موفقیت ایجاد شد",
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
                Data = null

            };
            return success;
        }
        public async Task<ResponseDTO> GetAllAvailableBarberShops()
        {
            var barberShopsExist = await _context.T_BarberShops.Include(c=>c.Barbers).Where(c => c.IsActive == true).ToListAsync();
            if (barberShopsExist.Any())
            {
               var barberShops = _mapper.Map<IEnumerable<BarberShopsDTO>>(barberShopsExist);
                var success = new ResponseDTO
                {
                    Message = "BarberShops successfully retrieved.",
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Data = barberShops
                };
                return success;
            }
            var error = new ResponseDTO
            {
                Message = "آرایشگاهی یافت نشد",
                IsSuccess = false,
                StatusCode = StatusCodes.Status200OK,
                Data = null
            };
            return error;
        }

        public async Task<ResponseDTO> GetAvailableBarberShop(Guid id)
        {
            var barberShopExist = await _context.T_BarberShops.Where(c=> c.IsActive == true && c.ID_Barbershop == id).FirstOrDefaultAsync();
            if(barberShopExist != null)
            {
                var barberShop = _mapper.Map<BarberShopsDTO>(barberShopExist);
                var success = new ResponseDTO
                {
                    Message = "BarberShop successfully retrieved.",
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Data = barberShop
                };
                return success;
            }
            var error = new ResponseDTO
            {
                Message = "No BarberShop available.",
                IsSuccess = false,
                StatusCode = StatusCodes.Status200OK,
                Data = null
            };
            return error;
        }
    }
}
