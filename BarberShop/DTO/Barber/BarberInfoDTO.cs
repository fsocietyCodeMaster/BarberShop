namespace BarberShop.DTO.Barber
{
    public class BarberInfoDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string? Bio { get; set; }
        public string? ImageUrl { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
