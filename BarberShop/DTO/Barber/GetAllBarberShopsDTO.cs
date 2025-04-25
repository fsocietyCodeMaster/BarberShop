using System.ComponentModel.DataAnnotations;

namespace BarberShop.DTO.Barber
{
    public class GetAllBarberShopsDTO
    {
        public Guid ID_Barbershop { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Description { get; set; }
    }
}
