using BarberShop.DTO.ResponseResult;
using BarberShop.Feature.Command.BarberShop.BarberShopForm;
using BarberShop.Feature.Command.BarberShop.UpdateBarberShopForm;
using BarberShop.Feature.Query.Barber.GetBarberShop;
using BarberShop.Feature.Query.BarberShop.GetBarberById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BarberShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarberShopController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<AuthController> _logger;

        public BarberShopController(ISender sender, ILogger<AuthController> logger)
        {
            _sender = sender;
            _logger = logger;
        }

        [HttpPost("barbershop")]
        [Authorize(Roles ="barbershop")]
        public async Task<IActionResult> BarberShopForm(BarberShopCommand command)
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
        [HttpGet("barbershoplists")]
        [Authorize(Roles = "barber,user")]
        public async Task<IActionResult> BarberShopLists()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _sender.Send(new GetBarberShopsQuery());
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
        [HttpGet("barbershop")]
        public async Task<IActionResult> BarberShop(Guid id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _sender.Send(new GetBarberShopByIdQuery(id));
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
        [HttpPost("Updatebarbershop")]
        [Authorize(Roles = "barbershop")]
        public async Task<IActionResult> UpdateBarberShopForm(UpdateBarberShopCommand command)
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
    }
}
