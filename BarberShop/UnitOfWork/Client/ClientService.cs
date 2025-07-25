using AutoMapper;
using BarberShop.Context;
using BarberShop.DTO.Appointment;
using BarberShop.DTO.ResponseResult;
using BarberShop.DTO.WorkSchedule;
using BarberShop.Migrations;
using BarberShop.Repository;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BarberShop.UnitOfWork.Client
{
    public class ClientService : IClient
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;

        public ClientService(BarberShopDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResponseDTO> GetBarberAppointment(string barberId, DateTime date)
        {
            if (string.IsNullOrWhiteSpace(barberId))
            {
                var error = new ResponseDTO
                {
                    Message = "There is a problem while sending parameter.",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Data = null
                };
                return error;
            }
            var reserved = await _context.T_Appointments.Where(c => c.T_Barber_ID == barberId && c.IsActive && c.AppointmentDate.Date == date.Date).ToListAsync();
            var mappedReserved = _mapper.Map<IEnumerable<ShowAppointmentClient>>(reserved);
            if (mappedReserved.Any())
            {
                var success = new ResponseDTO
                {
                    Message = "appointments is retrieved successfully.",
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Data = mappedReserved
                };
                return success;
            }
            else
            {
                var error = new ResponseDTO
                {
                    Message = "There are no appointments.",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Data = null
                };
                return error;
            }
        }

        public async Task<ResponseDTO> GetBarberSchedule(string barberId)
        {
            if (string.IsNullOrWhiteSpace(barberId))
            {

                var error = new ResponseDTO
                {
                    Message = "There is a problem while sending parameter.",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Data = null
                };
                return error;
            }
            var barberShcedule = await _context.T_BarberWorkSchedules.Where(c => c.T_Barber_ID == barberId).Select(c => new WorkScheduleDTO
            {
                ID_BarberWorkSchedule = c.ID_BarberWorkSchedule,
                StartTimeMorning = c.StartTimeMorning,
                EndTimeMorning = c.EndTimeMorning,
                StartTimeEvening = c.StartTimeEvening,
                EndTimeEvening = c.EndTimeEvening,
                ScopeTime = c.ScopeTime,
                SaturdayWork = c.SaturdayWork,
                SundayWork = c.SundayWork,
                MondayWork = c.MondayWork,
                TuesdayWork = c.TuesdayWork,
                WednesdayWork = c.WednesdayWork,
                ThursdayWork = c.ThursdayWork,
                FridayWork = c.FridayWork
            }).FirstOrDefaultAsync();

            if (barberShcedule != null)
            {
                var success = new ResponseDTO
                {
                    Message = "schedule is created successfully.",
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Data = barberShcedule
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

        public async Task<ResponseDTO> GetClientAppointment(string clientId)
        {
            if (string.IsNullOrWhiteSpace(clientId))
            {
                var error = new ResponseDTO
                {
                    Message = "There is a problem while sending parameter.",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Data = null
                };
                return error;
            }
            var reserved = await _context.T_Appointments.Where(c => c.T_Client_ID == clientId && c.IsActive).FirstOrDefaultAsync();
            if (reserved == null)
            {
                var error = new ResponseDTO
                {
                    Message = "There is no appointment.",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Data = null
                };
                return error;
            }
            var barber = await _context.Users.Include(c=> c.BarberShop).FirstOrDefaultAsync(c => c.Id == reserved.T_Barber_ID);
            if (barber == null)
            {
                var error = new ResponseDTO
                {
                    Message = "There is no barber.",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status404NotFound,
                    Data = null
                };
                return error;
            }
            var result = new ShowAppointmentClient
            {
                AppointmentDate = reserved.AppointmentDate,
                StartTime = reserved.StartTime,
                EndTime = reserved.EndTime,
                BarberName = barber.FullName,
                BarberShopName = barber.BarberShop.Name,
                BarberShopAddress = barber.BarberShop.Address,
                BarberShopPhone = barber.BarberShop.Phone,

            };
            var success = new ResponseDTO
            {
                Message = "appointments is retrieved successfully.",
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
                Data = result
            };
            return success;
        }
    }
}

