namespace BarberShop.DTO.Appointment
{
    public class ShowAppointmentClient
    {
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
