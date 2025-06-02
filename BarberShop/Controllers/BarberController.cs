using BarberShop.DTO.ResponseResult;
using BarberShop.Feature.Command.Barber.SelectBarberShop;
using BarberShop.Feature.Query.Barber.SelectBarber;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarberController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<BarberShopController> _logger;

        public BarberController(ISender sender,ILogger<BarberShopController> logger)
        {
            _sender = sender;
            _logger = logger;
        }
        [HttpPost("selectbarbershop")]
        [Authorize(Roles = "barber")]
        public async Task<IActionResult> SelectionOfBarberShop(Guid id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _sender.Send(new SelectBarberShopCommand(id));
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

        [HttpPost("selectbarber")]
        [Authorize(Roles = "user,barbershop")]
        public async Task<IActionResult> SelectionOfBarber(string id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _sender.Send(new SelectBarberQuery(id));
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
