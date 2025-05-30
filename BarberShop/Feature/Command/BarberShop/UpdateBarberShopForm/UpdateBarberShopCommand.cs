﻿using BarberShop.DTO.ResponseResult;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BarberShop.Feature.Command.BarberShop.UpdateBarberShopForm
{
    public record UpdateBarberShopCommand(Guid id) : IRequest<ResponseDTO>
    {
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

        public IFormFile? ImageUrl { get; set; }
    }
}
