using BarberShop.DTO.ResponseResult;
using BarberShop.Feature.Command.Barber.SelectBarberShop;
using BarberShop.Feature.Command.BarberSchedule;
using BarberShop.Feature.Command.BarberSchedule.CreateSchedule;
using BarberShop.Feature.Command.BarberSchedule.UpdateSchedule;
using BarberShop.Feature.Query.BarberSchedule.GetSchedule;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarberWorkScheduleController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<BarberShopController> _logger;

        public BarberWorkScheduleController(ISender sender, ILogger<BarberShopController> logger)
        {
            _sender = sender;
            _logger = logger;
        }

        [HttpPost("addworkschedule")]
        [Authorize(Roles = "barber")]
        public async Task<IActionResult> AddWorkSchedule(CreateBarberScheduleCommand command)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _sender.Send(command);
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

        [HttpPost("updateworkschedule")]
        [Authorize(Roles = "barber")]
        public async Task<IActionResult> UpdateWorkSchedule(UpdateBarberScheduleCommand command)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _sender.Send(command);
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
        [HttpGet("getworkschedule")]
        [Authorize(Roles = "barber")]
        public async Task<IActionResult> GetWorkSchedule()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _sender.Send(new GetScheduleQuery());
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
