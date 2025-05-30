﻿using BarberShop.DTO.ResponseResult;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace BarberShop.Feature.Command.User.CreateUser
{
    public record CreateUserCommand() : IRequest<ResponseDTO>
    {
        public string UserName { get; set; }


        [StringLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(60)]
        [Required]
        public string FullName { get; set; }
        [StringLength(250)]
        public string? Bio { get; set; }

        [StringLength(50)]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public string Role { get; set; }
    }
}
