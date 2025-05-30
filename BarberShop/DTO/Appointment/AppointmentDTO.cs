namespace BarberShop.DTO.Appointment
{
    public class AppointmentDTO
    {
        public Guid ID_Appointment { get; set; }
        public DateTime AppointmentDate { get; set; } 
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public string T_Client_ID { get; set; }

    }
}
