using BarberShop.Context;
using BarberShop.DTO.ResponseResult;
using BarberShop.DTO.WorkSchedule;
using BarberShop.Repository;
using Microsoft.EntityFrameworkCore;

namespace BarberShop.UnitOfWork.Client
{
    public class ClientService : IClient
    {
        private readonly BarberShopDbContext _context;

        public ClientService(BarberShopDbContext context)
        {
            _context = context;
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
    }
}
