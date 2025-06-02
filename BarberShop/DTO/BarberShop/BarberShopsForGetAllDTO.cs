using BarberShop.DTO.Barber;

namespace BarberShop.DTO.BarberShop
{
    public class BarberShopsForGetAllDTO
    {
        public Guid ID_Barbershop { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public List<BarberInfoForBarberShopDTO> Barbers { get; set; } = new List<BarberInfoForBarberShopDTO>();
    }
}
