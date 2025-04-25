using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarberShop.Model
{
    public class T_User : IdentityUser
    {
        [StringLength(60)]
        [Required]
        public string FullName { get; set; }
        [StringLength(250)]
        public string? Bio { get; set; }
        public string? ImageUrl { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public bool IsActive { get; set; }

        public Guid? T_BarberShop_ID { get; set; }
        [ForeignKey("T_BarberShop_ID")]
        public T_BarberShop? BarberShop { get; set; }

    }
}
