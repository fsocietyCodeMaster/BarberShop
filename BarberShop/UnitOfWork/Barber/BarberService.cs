﻿using AutoMapper;
using BarberShop.Context;
using BarberShop.DTO.Barber;
using BarberShop.DTO.BarberShop;
using BarberShop.DTO.ResponseResult;
using BarberShop.Model;
using BarberShop.Repository;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BarberShop.UnitOfWork.Barber
{
    public class BarberService : IBarber
    {
        private readonly BarberShopDbContext _context;
        private readonly IMapper _mapper;
        private readonly IBarberShop _barberShop;
        private readonly IHttpContextAccessor _httpContext;

        public BarberService(BarberShopDbContext context, IMapper mapper, IBarberShop barberShop, IHttpContextAccessor httpContext)
        {
            _context = context;
            _mapper = mapper;
            _barberShop = barberShop;
            _httpContext = httpContext;
        }
        public async Task<ResponseDTO> GetAllBarberShopForBarbers()
        {
            var barberShop = await _barberShop.GetAllAvailableBarberShops();
            if (barberShop != null)
            {
                var finalResult = _mapper.Map<IEnumerable<BarberShopsDTO>>(barberShop);
                var success = new ResponseDTO
                {
                    Message = "لیست آرایشگاه ها با موفقیت دریافت شد",
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Data = finalResult
                };
                return success;
            }
            else
            {
                var error = new ResponseDTO
                {
                    Message = "هیج  آرایشگاهی دریافت نشد",
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Data = null
                };
                return error;
            }
        }

        public async Task<ResponseDTO> SelectBarberShop(Guid id)
        {
            var user = _httpContext;
            if (user.HttpContext.User.Identity.IsAuthenticated)
            {
                var userId = user.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var finalUser = await _context.T_Users.FindAsync(userId);
                var barberShop = await _context.T_BarberShops.FindAsync(id);
                if (barberShop != null)
                {
                    if (finalUser.Status == UserStatus.Undefined)
                    {
                        finalUser.Status = UserStatus.Pending;
                        finalUser.T_BarberShop_ID = barberShop.ID_Barbershop;
                        await _context.SaveChangesAsync();
                        var result = new ResponseDTO
                        {
                            Message = "Your request to choose the barbershop is sent successfully,wait for acceptance.",
                            IsSuccess = true,
                            StatusCode = StatusCodes.Status200OK,
                            Data = null
                        };
                        return result;
                    }
                    else
                    {
                        var result = new ResponseDTO
                        {
                            Message = "You are either verified or rejected you can't continue.",
                            IsSuccess = true,
                            StatusCode = StatusCodes.Status200OK,
                            Data = new { barberId = finalUser.Id }
                        };
                        return result;
                    }
                }
                else
                {
                    var error = new ResponseDTO
                    {
                        Message = "No barbershop found.",
                        IsSuccess = true,
                        StatusCode = StatusCodes.Status200OK,
                        Data = null
                    };
                    return error;
                }

            }
            else
            {
                var error = new ResponseDTO
                {
                    Message = "User is not authenticated.",
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Data = null
                };
                return error;
            }
        }
    }
}