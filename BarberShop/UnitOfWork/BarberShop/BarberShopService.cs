using AutoMapper;
using BarberShop.Context;
using BarberShop.DTO.Barber;
using BarberShop.DTO.BarberShop;
using BarberShop.DTO.ResponseResult;
using BarberShop.Model;
using BarberShop.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BarberShop.UnitOfWork.BarberShop
{
    public class BarberShopService : IBarberShop
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<T_User> _userManager;

        public BarberShopService(BarberShopDbContext context, IMapper mapper, IHttpContextAccessor httpContext, UserManager<T_User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _httpContext = httpContext;
            _userManager = userManager;
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
                var url = "/" + fileName;
                var uploadUrl = Path.Combine(uploadFolder, fileName);
                using (var fileStream = new FileStream(uploadUrl, FileMode.Create))
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
            var barberShopsExist = await _context.T_BarberShops.AsSingleQuery().Include(c => c.Barbers).Where(c => c.IsActive == true).ToListAsync();
            if (barberShopsExist.Any())
            {
                var barberShops = _mapper.Map<IEnumerable<BarberShopsForGetAllDTO>>(barberShopsExist);
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
            var barberShopExist = await _context.T_BarberShops.AsSingleQuery().Include(c => c.Barbers).Where(c => c.IsActive == true && c.ID_Barbershop == id).FirstOrDefaultAsync();
            if (barberShopExist != null)
            {
                var barberShop = _mapper.Map<BarberShopDTO>(barberShopExist);
                var relativePath = $"{barberShop.ImageUrl}";
                var absoluteUrl = $"{_httpContext.HttpContext.Request.Scheme}://{_httpContext.HttpContext.Request.Host}{_httpContext.HttpContext.Request.PathBase}{relativePath}";
                barberShop.ImageUrl = absoluteUrl;
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
                    Message = " BarberShop found didn't found.",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status200OK,
                    Data = null
                };
                return error;
            }
        }

        public async Task<ResponseDTO> GetPendingBarbers()
        {
            if (_httpContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var barberShopUser = await _context.T_Users.FindAsync(userId);
                var barbershop = await _context.T_Users
                .Where(c => c.IsActive && c.Status == UserStatus.Pending && c.RequestedBarberShopId.HasValue)
                .Select(c => new BarberInfoForBarberShopDTO
                {
                    Id = c.Id,
                    FullName = c.FullName,
                    PhoneNumber = c.PhoneNumber,
                    Bio = c.Bio,
                    ImageUrl = c.ImageUrl,
                    Status = c.Status
                })
                .ToListAsync();
                if (barbershop.Any())
                {
                    var success = new ResponseDTO
                    {
                        Message = "Pending barbers are retrieved successfully.",
                        IsSuccess = true,
                        StatusCode = StatusCodes.Status200OK,
                        Data = barbershop
                    };
                    return success;
                }
                else
                {
                    var error = new ResponseDTO
                    {
                        Message = "No pending barbers found.",
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
                    Message = "No pending barbers found.",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status200OK,
                    Data = null
                };
                return error;
            }

        }

        public async Task<ResponseDTO> ApproveUser(string UserId, string Approve)
        {
            if (string.IsNullOrEmpty(UserId))
            {
                return new ResponseDTO()
                {
                    Message = "There is a problem with data sending.",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Data = null
                };
            }
            var barber = await _userManager.FindByIdAsync(UserId);

            if (barber == null)
            {
                var error = new ResponseDTO
                {
                    Message = "user not found.",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Data = null
                };
                return error;
            }
            if (barber.Status == UserStatus.Verified)
            {
                var error = new ResponseDTO
                {
                    Message = "User is verified before.",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Data = null
                };
                return error;
            }
            if (barber.Status == UserStatus.Pending && Approve == "verify")
            {
                barber.T_BarberShop_ID = barber.RequestedBarberShopId;
                barber.Status = UserStatus.Verified;
                barber.RequestedBarberShopId = null;
                await _userManager.UpdateAsync(barber);

                var success = new ResponseDTO
                {
                    Message = "User status is updated successfully.",
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Data = null
                };
                return success;
            }

            if (barber.Status == UserStatus.Pending && Approve == "reject")
            {
                barber.Status = UserStatus.Rejected;
                barber.RequestedBarberShopId = null;
                await _userManager.UpdateAsync(barber);

                var success = new ResponseDTO
                {
                    Message = "User status is updated successfully.",
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Data = null
                };
                return success;
            }
            else
            {
                return new ResponseDTO()
                {
                    Message = "Bad request",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Data = null
                };
            }

        }
    }
}
