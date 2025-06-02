using BarberShop.Context;
using BarberShop.DTO.ResponseResult;
using BarberShop.DTO.Slots;
using BarberShop.DTO.WorkSchedule;
using BarberShop.Model;
using BarberShop.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;

namespace BarberShop.UnitOfWork.BarberSchedule
{
    public class BarberScheduleService : IBarberSchedule
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly BarberShopDbContext _context;
        private readonly UserManager<T_User> _userManager;

        public BarberScheduleService(IHttpContextAccessor httpContextAccessor, BarberShopDbContext context, UserManager<T_User> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _userManager = userManager;
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
                var schedule = await _context.T_BarberWorkSchedules
                    .Where(c => c.T_Barber_ID == userId)
                    .Select(c => new WorkScheduleDTO
                    {
                        ID_BarberWorkSchedule = c.ID_BarberWorkSchedule,
                        StartTimeMorning = c.StartTimeMorning,
                        EndTimeMorning = c.EndTimeMorning,
                        StartTimeEvening = c.StartTimeEvening,
                        EndTimeEvening = c.EndTimeEvening,
                        SaturdayWork = c.SaturdayWork,
                        SundayWork = c.SundayWork,
                        MondayWork = c.MondayWork,
                        TuesdayWork = c.TuesdayWork,
                        WednesdayWork = c.WednesdayWork,
                        ThursdayWork = c.ThursdayWork,
                        FridayWork = c.FridayWork
                    }).FirstOrDefaultAsync();
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
                        schedule.StartTimeEvening = startEvening;
                        schedule.EndTimeEvening = endTimeEvening;
                        schedule.SaturdayWork = saturday;
                        schedule.SundayWork = sunday;
                        schedule.MondayWork = monday;
                        schedule.TuesdayWork = tuesday;
                        schedule.WednesdayWork = wednesday;
                        schedule.ThursdayWork = thursday;
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
        public async Task<ResponseDTO> GetAvailableTime(string id, DateTime date)
        {
            if (string.IsNullOrEmpty(id))
            {
                var error = new ResponseDTO
                {
                    Message = "There is a problem with parameter.",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status200OK,
                    Data = null
                };
                return error;
            }
            var barberExist = await _userManager.FindByIdAsync(id);
            var barberSchedule = await _context.T_BarberWorkSchedules.FirstOrDefaultAsync(c => c.T_Barber_ID == id);
            if (barberExist != null && barberSchedule != null)
            {
                var dayOfWeek = date.DayOfWeek;
                var fa = new CultureInfo("en"); // study about globalization class.
                bool isWorking = dayOfWeek switch
                {
                    DayOfWeek.Saturday => barberSchedule.SaturdayWork == true,
                    DayOfWeek.Sunday => barberSchedule.SundayWork == true,
                    DayOfWeek.Monday => barberSchedule.MondayWork == true,
                    DayOfWeek.Tuesday => barberSchedule.TuesdayWork == true,
                    DayOfWeek.Wednesday => barberSchedule.WednesdayWork == true,
                    DayOfWeek.Thursday => barberSchedule.ThursdayWork == true,
                    DayOfWeek.Friday => barberSchedule.FridayWork == true,
                    _ => false
                };

                if (!isWorking)
                {
                    var error = new ResponseDTO
                    {
                        Message = $"barber doesn't work at {dayOfWeek}",
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status400BadRequest,
                        Data = new List<AvailableTimeSlots>()

                    };
                    return error;
                }

                var appointments = await _context.T_Appointments
      .Where(c => c.T_Barber_ID == barberExist.Id && c.AppointmentDate == date && c.IsActive)
      .ToListAsync();

                var availableSlots = new List<AvailableTimeSlots>();
                var cutDuration = barberSchedule.ScopeTime;
                void GenerateSlots(TimeSpan start, TimeSpan end)
                {
                    for (var time = start; time + cutDuration <= end; time += cutDuration)
                    {
                        var overlap = appointments.Any(a =>
                            (time < a.EndTime) && (time + cutDuration > a.StartTime));

                        if (!overlap)
                        {
                            availableSlots.Add(new AvailableTimeSlots
                            {
                                Start = time,
                                End = time + cutDuration,
                                DayOfWeek = fa.DateTimeFormat.GetDayName(dayOfWeek)
                            });
                        }
                    }
                }

                GenerateSlots(barberSchedule.StartTimeMorning, barberSchedule.EndTimeMorning);
                GenerateSlots(barberSchedule.StartTimeEvening, barberSchedule.EndTimeEvening);

                var result = new ResponseDTO
                {
                    Message = "free times are retrieved.",
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Data = availableSlots
                };
                return result;
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

    }
}