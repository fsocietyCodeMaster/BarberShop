using BarberShop.Context;
using BarberShop.DTO.ResponseResult;
using BarberShop.Model;
using BarberShop.Repository;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BarberShop.UnitOfWork.BarberSchedule
{
    public class BarberScheduleService : IBarberSchedule
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly BarberShopDbContext _context;

        public BarberScheduleService(IHttpContextAccessor httpContextAccessor, BarberShopDbContext context)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public async Task<ResponseDTO> CreateSchedule(TimeSpan startTimeMorning, TimeSpan endTimeMorning, TimeSpan startEvening, TimeSpan endTimeEvening, TimeSpan scopeTime, bool saturday, bool sunday, bool monday, bool tuesday, bool wednesday, bool thursday, bool friday)
        {
            var user = _httpContextAccessor;
            if (user.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = user.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var schedule = new T_BarberWorkSchedule
                {
                    StartTimeMorning = startTimeMorning,
                    EndTimeMorning = endTimeMorning,
                    StartTimeEvening = startEvening,
                    EndTimeEvening = endTimeEvening,
                    ScopeTime = scopeTime,
                    SaturdayWork = saturday,
                    SundayWork = sunday,
                    MondayWork = monday,
                    TuesdayWork = tuesday,
                    WednesdayWork = wednesday,
                    ThursdayWork = thursday,
                    FridayWork = friday,
                    T_Barber_ID = userId
                };
                _context.Add(schedule);
                await _context.SaveChangesAsync();
                var success = new ResponseDTO
                {
                    Message = "schedule is created successfully.",
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
                    Message = "User is not authenticated.",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Data = null
                };
                return error;
            }
        }

        public async Task<ResponseDTO> GetSchedules()
        {
            var user = _httpContextAccessor;
            if (user.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = user.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var schedule = await _context.T_BarberWorkSchedules.FirstOrDefaultAsync(c => c.T_Barber_ID == userId);
                if (schedule != null)
                {
                    var success = new ResponseDTO
                    {
                        Message = "schedule is created successfully.",
                        IsSuccess = true,
                        StatusCode = StatusCodes.Status200OK,
                        Data = schedule
                    };
                    return success;
                }
                else
                {

                    var error = new ResponseDTO
                    {
                        Message = "There is no schedule.",
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

        public async Task<ResponseDTO> UpdateSchedule(TimeSpan startTimeMorning, TimeSpan endTimeMorning, TimeSpan startEvening, TimeSpan endTimeEvening, TimeSpan scopeTime, bool saturday, bool sunday, bool monday, bool tuesday, bool wednesday, bool thursday, bool friday)
        {
            var user = _httpContextAccessor;
            if (user.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = user.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId != null)
                {
                    var schedule = await _context.T_BarberWorkSchedules.FirstOrDefaultAsync(c => c.T_Barber_ID == userId);
                    if (schedule != null)
                    {
                        schedule.StartTimeMorning = startTimeMorning;
                        schedule.EndTimeMorning = endTimeMorning;
                        schedule.StartTimeEvening = schedule.EndTimeEvening;
                        schedule.SaturdayWork = saturday;
                        schedule.SundayWork = sunday;
                        schedule.MondayWork = monday;
                        schedule.TuesdayWork = tuesday;
                        schedule.WednesdayWork = wednesday;
                        schedule.FridayWork = friday;
                        _context.Update(schedule);
                        await _context.SaveChangesAsync();
                        var success = new ResponseDTO
                        {
                            Message = "schedule is updated successfully.",
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
                            Message = "Schedule is not empty.",
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
                        Message = "There is no user.",
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