namespace BarberShop.DTO.Appointment
{
    public class ShowAppointmentClient
    {
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string? BarberName { get; set; }
        public string? BarberShopName { get; set; }
        public string? BarberShopAddress { get; set; }
        public string? BarberShopPhone { get; set; }

    }
}
