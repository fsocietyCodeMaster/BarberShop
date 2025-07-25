﻿using BarberShop.DTO.ResponseResult;
using BarberShop.Feature.Query.Client.GetBarberAppointment;
using BarberShop.Feature.Query.Client.GetBarberSchedule;
using BarberShop.Feature.Query.Client.GetClientAppointment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BarberShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<BarberShopController> _logger;

        public ClientController(ISender sender, ILogger<BarberShopController> logger)
        {
            _sender = sender;
            _logger = logger;
        }

        [HttpGet("barberSchedule")]
        [Authorize(Roles ="user")]
        public async Task<IActionResult> GetBarberScheduel(string id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _sender.Send(new GetBarberSheduleQuery(id));
                    if (result.IsSuccess)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest(result);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "an error occurred.");

                    var error = new ResponseDTO
                    {
                        Message = "خطای سرور رخ داده است. اگر مشکل ادامه داشت، لطفاً با پشتیبانی تماس بگیرید",
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status500InternalServerError
                    };

                    return BadRequest(error);
                }
            }
            else
            {
                return BadRequest("برخی از ورودی ها نامعتبر هستند");
            }
        }


        [HttpGet("barberappointment")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetBarberAppointment(string id, DateTime date)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _sender.Send(new GetBarberAppointmentQuery(id,date));
                    if (result.IsSuccess)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest(result);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "an error occurred.");

                    var error = new ResponseDTO
                    {
                        Message = "خطای سرور رخ داده است. اگر مشکل ادامه داشت، لطفاً با پشتیبانی تماس بگیرید",
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status500InternalServerError
                    };

                    return BadRequest(error);
                }
            }
            else
            {
                return BadRequest("برخی از ورودی ها نامعتبر هستند");
            }
        }


        [HttpGet("clientappointment")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> GetClientAppointment()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var result = await _sender.Send(new GetClientAppointmentQuery(id));
                    if (result.IsSuccess)
                    {
                        return Ok(result);
                    }
                    else
                    {
                        return BadRequest(result);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "an error occurred.");

                    var error = new ResponseDTO
                    {
                        Message = "خطای سرور رخ داده است. اگر مشکل ادامه داشت، لطفاً با پشتیبانی تماس بگیرید",
                        IsSuccess = false,
                        StatusCode = StatusCodes.Status500InternalServerError
                    };

                    return BadRequest(error);
                }
            }
            else
            {
                return BadRequest("برخی از ورودی ها نامعتبر هستند");
            }
        }
    }
}
