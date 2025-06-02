namespace BarberShop.DTO.WorkSchedule
{
    public class WorkScheduleDTO
    {
        public Guid ID_BarberWorkSchedule { get; set; }
        public TimeSpan StartTimeMorning { get; set; }
        public TimeSpan StartTimeEvening { get; set; }
        public TimeSpan EndTimeMorning { get; set; }
        public TimeSpan EndTimeEvening { get; set; }
        public TimeSpan ScopeTime { get; set; }
        public bool SaturdayWork { get; set; }
        public bool SundayWork { get; set; }
        public bool MondayWork { get; set; }
        public bool TuesdayWork { get; set; }
        public bool WednesdayWork { get; set; }
        public bool ThursdayWork { get; set; }
        public bool FridayWork { get; set; }
    }
}
