namespace BarberShop.DTO.Slots
{
    public class AvailableTimeSlots
    {
        public string DayOfWeek { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan End { get; set; }
    }
}
