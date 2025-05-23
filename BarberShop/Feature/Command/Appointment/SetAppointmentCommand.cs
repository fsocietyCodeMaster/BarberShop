﻿using BarberShop.DTO.ResponseResult;
using MediatR;
using System.Security.Claims;

namespace BarberShop.Feature.Command.Appointment
{
    public record SetAppointmentCommand : IRequest<ResponseDTO>
    {
        public string ID_Barber { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
