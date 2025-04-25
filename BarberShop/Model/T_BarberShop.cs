using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShop.Model
{
    public class T_BarberShop
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID_Barbershop { get; set; }
        
        [StringLength(150)]
        [Required]
        public string Name { get; set; }
        [StringLength(250)]
        [Required]
        public string Address { get; set; }

        public string Phone { get; set; }

        [StringLength(500)]
        [Required]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        public Guid OwnerId { get; set; }
  
        public List<T_User> Barbers { get; set; } = new List<T_User>();
    }
}
