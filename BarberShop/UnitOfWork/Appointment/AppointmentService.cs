using BarberShop.Context;
using BarberShop.DTO.Appointment;
using BarberShop.DTO.ResponseResult;
using BarberShop.Model;
using BarberShop.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BarberShop.UnitOfWork.Appointment
{
    public class AppointmentService : IAppointment
    {
        private readonly IHttpContextAccessor _httpContext;
        private readonly BarberShopDbContext _context;
        private readonly UserManager<T_User> _userManager;


        public AppointmentService(IHttpContextAccessor httpContext, BarberShopDbContext context, UserManager<T_User> userManager)
        {
            _httpContext = httpContext;
            _context = context;
            _userManager = userManager;
        }
        public async Task<ResponseDTO> GetPendingAppointments()
        {
            if (!_httpContext.HttpContext.User.Identity.IsAuthenticated)
            {
                return new ResponseDTO()
                {
                    Message = ".کاربر وارد سیستم نشده است",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Data = null
                };
            }
            var barberId = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(barberId))
            {
                return new ResponseDTO()
                {
                    Message = ".بازیابی اطلاعات کاربر امکان‌پذیر نیست",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Data = null
                };
            }
            var barberAppointment = await _context.T_Appointments.Where(c => c.T_Barber_ID == barberId).ToListAsync();
            if (barberAppointment.Count > 0 && barberAppointment.Any())
            {
                if (barberAppointment.Any(c => c.Status == AppointmentStatus.Pending))
                {
                    var projectedBarber = barberAppointment.Select(c => new AppointmentDTO
                    {
                        ID_Appointment = c.ID_Appointment,
                        AppointmentDate = c.AppointmentDate,
                        StartTime = c.StartTime,
                        EndTime = c.EndTime,
                        CreatedAt = c.CreatedAt,
                        Status = c.Status.ToString(),
                        T_Client_ID = c.T_Client_ID,
                    });

                    var success = new ResponseDTO
                    {
                        Message = "Pending appointments are retrieved.",
                        IsSuccess = true,
                        StatusCode = StatusCodes.Status200OK,
                        Data = projectedBarber
                    };
                    return success;
                }
                var error = new ResponseDTO
                {
                    Message = "No pending status found.",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Data = null
                };
                return error;
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

        public async Task<ResponseDTO> SetAppointment(string barberId, DateTime appointmentTime, TimeSpan start, TimeSpan end)
        {
            if (!_httpContext.HttpContext.User.Identity.IsAuthenticated)
            {
                return new ResponseDTO()
                {
                    Message = ".کاربر وارد سیستم نشده است",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status401Unauthorized,
                    Data = null
                };
            }
            var client = _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(client))
            {
                return new ResponseDTO()
                {
                    Message = ".بازیابی اطلاعات کاربر امکان‌پذیر نیست",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Data = null
                };
            }
            var barberExist = await _userManager.FindByIdAsync(barberId);
            var barberSchedule = await _context.T_BarberWorkSchedules.FirstOrDefaultAsync(c => c.T_Barber_ID == barberExist.Id);

            if (barberExist == null && barberSchedule == null)
            {
                return new ResponseDTO
                {
                    Message = "No barber and schedule found.",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Data = null
                };
            }
            var newAppointment = new T_Appointment
            {
                AppointmentDate = appointmentTime,
                StartTime = start,
                EndTime = end,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                T_Barber_ID = barberId,
                T_Client_ID = client,
                Status = AppointmentStatus.Pending
            };
            _context.Add(newAppointment);
            await _context.SaveChangesAsync();

            return new ResponseDTO
            {
                Message = "Appointment has been successfully scheduled.",
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
                Data = null
            };

        }

    }

}

