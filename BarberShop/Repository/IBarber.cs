﻿using BarberShop.DTO.ResponseResult;

namespace BarberShop.Repository
{
    public interface IBarber
    {
        Task<ResponseDTO> SelectBarberShop(Guid id);
    }
}
