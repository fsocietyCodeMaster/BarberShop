namespace BarberShop.DTO.User
{
    public class UserInfoDTO
    {
        public string FullName { get; set; }
        public string? Bio { get; set; }
        public string? ImageUrl { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public string? UserName { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
