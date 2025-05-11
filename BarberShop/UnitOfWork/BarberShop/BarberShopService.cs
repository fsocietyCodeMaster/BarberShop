using AutoMapper;
using BarberShop.Context;
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

        public BarberShopService(BarberShopDbContext context, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContext;
        }
        public async Task<ResponseDTO> ShopForm(string Name, string Address, string Phone, string Description, IFormFile ImageUrl)
        {
            var httpContext = _httpContext;
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
            if (ImageUrl != null)
            {
                if (ImageUrl.Length == 0)
                {
                    var error = new ResponseDTO
                    {
                        Message = "فایل آپلود شده خالی است",
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Data = null
                    };
                    return error;
                }
                if (ImageUrl.Length > 1048576)
                {
                    var error = new ResponseDTO
                    {
                        Message = "حجم فایل تصویر بیشتر از ۱ مگابایت است",
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Data = null
                    };
                    return error;
                }
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var fileExtension = Path.GetExtension(ImageUrl.FileName);
                if (!allowedExtensions.Contains(fileExtension.ToLower()))
                {
                    return new ResponseDTO
                    {
                        Message = "نوع فایل نامعتبر است",
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Data = null
                    };
                }
                var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }
                var fileName = Guid.NewGuid().ToString() + fileExtension;
                var url = Path.Combine(uploadFolder, fileName);
                using (var fileStream = new FileStream(url, FileMode.Create))
                {
                    await ImageUrl.CopyToAsync(fileStream);
                }
                barberShop.ImageUrl = url;
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
            else
            {
                var error = new ResponseDTO
                {
                    Message = "No image found",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status200OK,
                    Data = null
                };
                return error;
            }
        }
        public async Task<ResponseDTO> GetAllAvailableBarberShops()
        {
            var barberShopsExist = await _context.T_BarberShops.Include(c => c.Barbers).Where(c => c.IsActive == true).ToListAsync();
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
            var barberShopExist = await _context.T_BarberShops.Where(c => c.IsActive == true && c.ID_Barbershop == id).FirstOrDefaultAsync();
            if (barberShopExist != null)
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

        public async Task<ResponseDTO> UpdateBarberShopForm(Guid id, string Name, string Address, string Phone, string Description, IFormFile ImageUrl)
        {
            var barberShopExist = await _context.T_BarberShops.FirstOrDefaultAsync(c => c.ID_Barbershop == id);
            if (barberShopExist != null)
            {
                if (ImageUrl != null)
                {
                    if (ImageUrl.Length == 0)
                    {
                        var error = new ResponseDTO
                        {
                            Message = "فایل آپلود شده خالی است",
                            IsSuccess = false,
                            StatusCode = StatusCodes.Status400BadRequest,
                            Data = null
                        };
                        return error;
                    }
                    if (ImageUrl.Length > 1048576)
                    {
                        var error = new ResponseDTO
                        {
                            Message = "حجم فایل تصویر بیشتر از ۱ مگابایت است",
                            IsSuccess = false,
                            StatusCode = StatusCodes.Status400BadRequest,
                            Data = null
                        };
                        return error;
                    }
                    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                    var fileExtension = Path.GetExtension(ImageUrl.FileName);
                    if (!allowedExtensions.Contains(fileExtension.ToLower()))
                    {
                        return new ResponseDTO
                        {
                            Message = "نوع فایل نامعتبر است",
                            IsSuccess = false,
                            StatusCode = StatusCodes.Status400BadRequest,
                            Data = null
                        };
                    }
                    var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                    if (!Directory.Exists(uploadFolder))
                    {
                        Directory.CreateDirectory(uploadFolder);
                    }
                    var fileName = Guid.NewGuid().ToString() + fileExtension;
                    var url = Path.Combine(uploadFolder, fileName);
                    using (var fileStream = new FileStream(url, FileMode.Create))
                    {
                        await ImageUrl.CopyToAsync(fileStream);
                    }
                    barberShopExist.ImageUrl = url;
                    barberShopExist.Name = Name;
                    barberShopExist.Address = Address;
                    barberShopExist.Phone = Phone;
                    barberShopExist.Description = Description;
                    await _context.SaveChangesAsync();
                    var success = new ResponseDTO
                    {
                        Message = "BarberShop successfully updated.",
                        IsSuccess = true,
                        StatusCode = StatusCodes.Status200OK,
                        Data = null
                    };
                    return success;
                }
                else
                {
                    var error = new ResponseDTO
                    {
                        Message = "No image found.",
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status200OK,
                        Data = null
                    };
                    return error;
                }
            }
            else
            {
                var error = new ResponseDTO
                {
                    Message = " BarberShop found didnt found.",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status200OK,
                    Data = null
                };
                return error;
            }
        }
    }
}