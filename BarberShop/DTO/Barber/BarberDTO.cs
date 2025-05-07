namespace BarberShop.DTO.Barber
{
    public class BarberDTO
    {
        public string FullName { get; set; }
        public string? Bio { get; set; }
        public string? ImageUrl { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public bool IsActive { get; set; }
    }
}
