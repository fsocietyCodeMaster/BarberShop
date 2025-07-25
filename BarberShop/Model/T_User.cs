﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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
        public bool IsActive { get; set; }
        public UserStatus? Status { get; set; }
        public Guid? RequestedBarberShopId { get; set; }
        public Guid? T_BarberShop_ID { get; set; }
        [ForeignKey("T_BarberShop_ID")]
        [JsonIgnore]
        public T_BarberShop? BarberShop { get; set; }
        public ICollection<T_Appointment>? Appointments { get; set; } = new List<T_Appointment>();

    }
    public enum UserStatus
    {
        Pending,
        Verified,
        Rejected,
        Undefined
    }
}
