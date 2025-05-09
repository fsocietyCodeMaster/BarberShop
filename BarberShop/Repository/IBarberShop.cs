﻿using BarberShop.DTO.ResponseResult;

namespace BarberShop.Repository
{
    public interface IBarberShop
    {
        Task<ResponseDTO> ShopForm(string Name, string Address, string Phone, string Description);
        Task<ResponseDTO> GetAllAvailableBarberShops();
        Task<ResponseDTO> GetAvailableBarberShop(Guid id);

    }
}
