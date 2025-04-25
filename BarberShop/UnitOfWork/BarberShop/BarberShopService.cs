using BarberShop.Context;
using BarberShop.DTO.ResponseResult;
using BarberShop.Model;
using BarberShop.Repository;

namespace BarberShop.UnitOfWork.BarberShop
{
    public class BarberShopService : IBarberShop
    {
        private readonly BarberShopDbContext _context;

        public BarberShopService(BarberShopDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseDTO> ShopForm(string Name, string Address, string Phone, string Description, bool IActive)
        {

            var barberShop = new T_BarberShop
            {
                Name = Name,
                Address = Address,
                Phone = Phone,
                Description = Description,
                IsActive = IActive
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
    }
}
