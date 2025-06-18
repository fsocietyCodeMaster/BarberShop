using BarberShop.Model;

namespace BarberShop.DTO.Barber
{
    public class BarberInfoDTO
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string? Bio { get; set; }
        public string? ImageUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public TimeSpan StartTimeMorning { get; set; }
        public TimeSpan EndTimeMorning { get; set; }
        public TimeSpan StartTimeEvening { get; set; }
        public TimeSpan EndTimeEvening { get; set; }
        public TimeSpan ScopeTime { get; set; }
        public bool SaturdayWork { get; set; }
        public bool SundayWork { get; set; }
        public bool MondayWork { get; set; }
        public bool TuesdayWork { get; set; }
        public bool WednesdayWork { get; set; }
        public bool ThursdayWork { get; set; }
        public bool FridayWork { get; set; }
        public UserStatus? Status { get; set; }

    }
}
