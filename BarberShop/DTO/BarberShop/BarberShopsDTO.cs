using BarberShop.DTO.Barber;


namespace BarberShop.DTO.BarberShop
{
    public class BarberShopsDTO
    {
        public Guid ID_Barbershop { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string OwnerId { get; set; }

        public List<BarberInfoDTO> Barbers { get; set; } = new List<BarberInfoDTO>();
    }
}
