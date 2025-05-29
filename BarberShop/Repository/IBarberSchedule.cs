using BarberShop.DTO.ResponseResult;

namespace BarberShop.Repository
{
    public interface IBarberSchedule
    {
        Task<ResponseDTO> CreateSchedule(TimeSpan startTimeMorning, TimeSpan endTimeMorning,
TimeSpan startEvening, TimeSpan endTimeEvening, TimeSpan scopeTime, bool saturday,
bool sunday, bool monday, bool tuesday, bool wednesday, bool thursday, bool friday);
        Task<ResponseDTO> UpdateSchedule(TimeSpan startTimeMorning, TimeSpan endTimeMorning,
TimeSpan startEvening, TimeSpan endTimeEvening, TimeSpan scopeTime, bool saturday,
bool sunday, bool monday, bool tuesday, bool wednesday, bool thursday, bool friday);

        Task<ResponseDTO> GetSchedules();

    }
}
