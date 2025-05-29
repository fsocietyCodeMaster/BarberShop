using AutoMapper;
using BarberShop.Context;
using BarberShop.DTO.Barber;
using BarberShop.DTO.BarberShop;
using BarberShop.DTO.ResponseResult;
using BarberShop.DTO.Slots;
using BarberShop.Model;
using BarberShop.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BarberShop.UnitOfWork.Barber
{
    public class BarberService : IBarber
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;
        private readonly IBarberShop _barberShop;
        private readonly IHttpContextAccessor _httpContext;
        private readonly UserManager<T_User> _userManager;

        public BarberService(BarberShopDbContext context, IMapper mapper, IBarberShop barberShop, IHttpContextAccessor httpContext, UserManager<T_User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _barberShop = barberShop;
            _httpContext = httpContext;
            _userManager = userManager;
        }
        public async Task<ResponseDTO> GetAllBarberShopForBarbers()
        {
            var barberShop = await _barberShop.GetAllAvailableBarberShops();
            if (barberShop != null)
            {
                var finalResult = _mapper.Map<IEnumerable<BarberShopsDTO>>(barberShop);
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
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Data = null
                };
                return error;
            }
        }

        //public async Task<ResponseDTO> GetBarber(string id)
        //{
        //    if (string.IsNullOrEmpty(id))
        //    {
        //        var error = new ResponseDTO
        //        {
        //            Message = "There is a problem with parameter.",
        //            IsSuccess = false,
        //            StatusCode = StatusCodes.Status400BadRequest,
        //            Data = null
        //        };
        //        return error;
        //    }
        //    var barberExist =  await _userManager.FindByIdAsync(id);
        //    if(barberExist != null)
        //    {
        //        var schedule = await _context.T_BarberWorkSchedules.Where(c => c.T_Barber_ID == barberExist.Id).Select(c => new BarberInfoDTO
        //        {
        //            Id = c.T_Barber_ID,
        //            FullName = barberExist.FullName,
        //            Bio = barberExist.Bio,
        //            ImageUrl = barberExist.ImageUrl,
        //            PhoneNumber = barberExist.PhoneNumber,
        //            StartTimeMorning = c.StartTimeMorning,
        //            EndTimeMorning = c.EndTimeMorning,
        //            StartTimeEvening = c.StartTimeEvening,
        //            EndTimeEvening = c.EndTimeEvening,
        //            SaturdayWork = c.SaturdayWork,
        //            SundayWork = c.SundayWork,
        //            MondayWork = c.MondayWork,
        //            TuesdayWork = c.TuesdayWork,
        //            WednesdayWork = c.WednesdayWork,
        //            ThursdayWork = c.ThursdayWork,
        //            FridayWork = c.FridayWork,
        //            ScopeTime = c.ScopeTime

        //        }).FirstOrDefaultAsync();
        //        var success = new ResponseDTO
        //        {
        //            Message = "Barber is retrieved successfully.",
        //            IsSuccess = true,
        //            StatusCode = StatusCodes.Status200OK,
        //            Data = schedule
        //        };
        //        return success;
        //    }
        //    return new ResponseDTO()
        //    {
        //        Message = "No barber found.",
        //        IsSuccess = false,
        //        StatusCode = StatusCodes.Status200OK,
        //        Data = null
        //    };

        //}

        public async Task<ResponseDTO> SelectBarberShop(Guid id)
        {
            var user = _httpContext;
            if (user.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = user.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var finalUser = await _context.T_Users.FindAsync(userId);
                var barberShop = await _context.T_BarberShops.FindAsync(id);
                if (barberShop != null)
                {
                    if (finalUser.Status == UserStatus.Undefined)
                    {
                        finalUser.Status = UserStatus.Pending;
                        finalUser.T_BarberShop_ID = barberShop.ID_Barbershop;
                        await _context.SaveChangesAsync(); // is enough cause its tracking by ef.
                        var result = new ResponseDTO
                        {
                            Message = "Your request to choose the barbershop is sent successfully,wait for acceptance.",
                            IsSuccess = true,
                            StatusCode = StatusCodes.Status200OK,
                            Data = null
                        };
                        return result;
                    }
                    else
                    {
                        var result = new ResponseDTO
                        {
                            Message = "You are either verified or rejected you can't continue.",
                            IsSuccess = true,
                            StatusCode = StatusCodes.Status200OK,
                            Data = new { barberId = finalUser.Id }
                        };
                        return result;
                    }
                }
                else
                {
                    var error = new ResponseDTO
                    {
                        Message = "No barbershop found.",
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Data = null
                    };
                    return error;
                }

            }
            else
            {
                var error = new ResponseDTO
                {
                    Message = "User is not authenticated.",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Data = null
                };
                return error;
            }
        }

        public async Task<ResponseDTO> UpdateBarber(string bio)
        {
            var user = _httpContext;
            if (user.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = user.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var barber = await _context.T_Users.FindAsync(userId);
                var barberSchedule = await _context.T_BarberWorkSchedules.FirstOrDefaultAsync(c => c.T_Barber_ID == userId);
                if (barber != null && barberSchedule != null)
                {
                    barber.Bio = bio;
                    await _userManager.UpdateAsync(barber);
                    var success = new ResponseDTO
                    {
                        Message = "لیست آرایشگاه ها با موفقیت دریافت شد",
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
                        Message = "There is a no barber.",
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Data = null
                    };
                    return error;
                }
            }
            else
            {
                var error = new ResponseDTO
                {
                    Message = "User is not authenticated.",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Data = null
                };
                return error;
            }

        }
    }
}