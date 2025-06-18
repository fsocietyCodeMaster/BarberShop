using BarberShop.Model;

namespace BarberShop.DTO.Barber
{
    public class BarberInfoForBarberShopDTO
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string? Bio { get; set; }
        public string? ImageUrl { get; set; }
        public string? PhoneNumber { get; set; }
        public UserStatus? Status { get; set; }

    }
}
