using AutoMapper;
using Azure;
using BarberShop.Context;
using BarberShop.DTO.Appointment;
using BarberShop.DTO.Barber;
using BarberShop.DTO.ResponseResult;
using BarberShop.DTO.Slots;
using BarberShop.DTO.User;
using BarberShop.Model;
using BarberShop.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;

namespace BarberShop.UnitOfWork.User
{
    public class UserService : IUser
    {
        private readonly BarberShopDbContext _context;
        private readonly UserManager<T_User> _userManager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public UserService(BarberShopDbContext context, UserManager<T_User> userManager, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public async Task<ResponseDTO> DeleteUserAsync(Guid id)
        {
            var user = await _context.T_Users.FindAsync(id);
            if (user != null)
            {
                _context.Remove(user);
                await _context.SaveChangesAsync();
                var success = new ResponseDTO
                {
                    Message = "کاربر با موفقیت حذف شد",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status500InternalServerError

                };
                return success;
            }
            else
            {
                var error = new ResponseDTO
                {
                    Message = "کاربر یافت نشد",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status500InternalServerError
                };
                return error;
            }
        }


        public async Task<ResponseDTO> GetUser(ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
            {
                return new ResponseDTO()
                {
                    Message = ".کاربر وارد سیستم نشده است",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Data = null
                };
            }
            var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = user.FindFirst(ClaimTypes.Role)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return new ResponseDTO()
                {
                    Message = ".بازیابی اطلاعات کاربر امکان‌پذیر نیست",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Data = null
                };
            }
            var userExist = await _userManager.FindByIdAsync(userId);
            if (userExist == null)
            {
                return new ResponseDTO()
                {
                    Message = ".کاربری با این شناسه وجود ندارد",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Data = null
                };

            }
            if (userRole == "user")
            {
                var success = new ResponseDTO
                {
                    Message = ".کاربری با این شناسه وجود دارد و تأیید شده است",
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Data = new UserInfoDTO
                    {
                        FullName = userExist.FullName,
                        ImageUrl = userExist.ImageUrl,
                        UserName = userExist.UserName,
                        PhoneNumber = userExist.PhoneNumber,
                    }
                };
                return success;
            }
            else if (userRole == "barber")
            {
                var success = new ResponseDTO
                {
                    Message = ".کاربری با این شناسه وجود دارد و تأیید شده است",
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Data = new UserInfoDTO
                    {
                        FullName = userExist.FullName,
                        ImageUrl = userExist.ImageUrl,
                        UserName = userExist.UserName,
                        PhoneNumber = userExist.PhoneNumber,
                        Bio = userExist.Bio,
                    }
                };
                return success;
            }
            else if (userRole == "barbershop")
            {
                var success = new ResponseDTO
                {
                    Message = ".کاربری با این شناسه وجود دارد و تأیید شده است",
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Data = new UserInfoDTO
                    {
                        FullName = userExist.FullName,
                        ImageUrl = userExist.ImageUrl,
                        UserName = userExist.UserName,
                        PhoneNumber = userExist.PhoneNumber,
                    }
                };
                return success;

            }
            else
            {
                return new ResponseDTO()
                {
                    Message = "There is problem while sending the data.",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status200OK,
                    Data = null
                };
            }

        }

        public async Task<ResponseDTO> SelectBarber(string id)
        {
            var barberExist = await _context.T_Users.FirstOrDefaultAsync(c => c.Id == id);
            if (barberExist != null)
            {
                var schedule = await _context.T_BarberWorkSchedules.Where(c => c.T_Barber_ID == barberExist.Id).Select(c=> new BarberInfoDTO
                {
                    Id = c.T_Barber_ID,
                    FullName = barberExist.FullName,
                    Bio = barberExist.Bio,
                    ImageUrl = barberExist.ImageUrl,
                    PhoneNumber = barberExist.PhoneNumber,
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
                    FridayWork = c.FridayWork,
                    ScopeTime = c.ScopeTime

                }).FirstOrDefaultAsync();
                var success = new ResponseDTO
                {
                    Message = "Barber is retrieved successfully.",
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Data = schedule
                };
                return success;
            }
            return new ResponseDTO()
            {
                Message = "No barber found.",
                IsSuccess = false,
                StatusCode = StatusCodes.Status200OK,
                Data = null
            };


        }

        //public async Task<ResponseDTO> SetAppointment(string barberId, DateTime appointmentTime, TimeSpan start, TimeSpan end)
        //{
        //    if (!_httpContext.HttpContext.User.Identity.IsAuthenticated)
        //    {
        //        return new ResponseDTO()
        //        {
        //            Message = ".کاربر وارد سیستم نشده است",
        //            IsSuccess = false,
        //            StatusCode = StatusCodes.Status401Unauthorized,
        //            Data = null
        //        };
        //    }
        //    var client = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        //    if (string.IsNullOrEmpty(client))
        //    {
        //        return new ResponseDTO()
        //        {
        //            Message = ".بازیابی اطلاعات کاربر امکان‌پذیر نیست",
        //            IsSuccess = false,
        //            StatusCode = StatusCodes.Status400BadRequest,
        //            Data = null
        //        };
        //    }
        //    var barberExist = await _userManager.FindByIdAsync(barberId);
        //    if (barberExist == null)
        //    {
        //        return new ResponseDTO
        //        {
        //            Message = "No barber found.",
        //            IsSuccess = false,
        //            StatusCode = StatusCodes.Status200OK,
        //            Data = null
        //        };
        //    }

        //    if (barberExist.Appointments.Any(c => c.IsActive && start < c.EndTime && end > c.StartTime))
        //    {
        //        return new ResponseDTO
        //        {
        //            Message = "The selected time overlaps with an existing appointment.",
        //            IsSuccess = false,
        //            StatusCode = StatusCodes.Status200OK,
        //            Data = null
        //        };
        //    }
        //    if (start < barberExist.StartTime || end > barberExist.EndTime)
        //    {
        //        return new ResponseDTO
        //        {
        //            Message = "The selected time is outside the barber's working hours.",
        //            IsSuccess = false,
        //            StatusCode = StatusCodes.Status400BadRequest,
        //            Data = null
        //        };
        //    }
        //    var newAppointment = new T_Appointment
        //    {
        //        AppointmentDate = appointmentTime,
        //        StartTime = start,
        //        EndTime = end,
        //        IsActive = true,
        //        CreatedAt = DateTime.UtcNow,
        //        T_Barber_ID = barberId,
        //        T_Client_ID = client,
        //        Status = AppointmentStatus.Pending
        //    };
        //    _context.Add(newAppointment);
        //    await _context.SaveChangesAsync();

        //    return new ResponseDTO
        //    {
        //        Message = "Appointment has been successfully scheduled.",
        //        IsSuccess = true,
        //        StatusCode = StatusCodes.Status200OK,
        //        Data = null
        //    };

        //}


        public async Task<ResponseDTO> UpdateUserAsync(string Username, string FullName, string PhoneNumber, IFormFile ImageUrl, string? bio)
        {
            var userContext = _httpContext;
            if (userContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var userRole = userContext.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;
                var user = await _userManager.FindByNameAsync(Username);
                if (user != null)
                {
                    if (ImageUrl != null)
                    {
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
                        user.ImageUrl = url;
                    }
                    user.FullName = FullName;
                    user.PhoneNumber = PhoneNumber;
                    if (userRole == "barber")
                    {
                        user.Bio = bio;
                    }
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                    var success = new ResponseDTO
                    {
                        Message = "کاربر با موفقیت به‌روزرسانی شد",
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
                        Message = "کاربر یافت نشد",
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status404NotFound,
                        Data = null
                    };
                }
            }
            else
            {
                return new ResponseDTO()
                {
                    Message = "User is not authenticated.",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Data = null
                };
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
                    
                var appointments = barberExist.Appointments
                    .Where(c => c.AppointmentDate == date && c.IsActive)
                    .ToList();

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