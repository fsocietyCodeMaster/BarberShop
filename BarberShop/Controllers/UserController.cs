using BarberShop.DTO.ResponseResult;
using BarberShop.Feature.Command.Appointment;
using BarberShop.Feature.Command.User.DeleteUser;
using BarberShop.Feature.Command.User.SelectBarber;
using BarberShop.Feature.Command.User.UpdateUser;
using BarberShop.Feature.Query.User.GetAvailableTime;
using BarberShop.Feature.Query.User.GetUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<AuthController> _logger;
        public UserController(ISender sender, ILogger<AuthController> logger)
        {
            _sender = sender;
            _logger = logger;
        }
        [HttpPost("updateuser")]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
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

        [HttpPost("deleteuser")]
        public async Task<IActionResult> DeleteUser([FromQuery]DeleteUserCommand command)
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
        [HttpGet("getuser")]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _sender.Send(new GetUserCommand(User));
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
                    var result = await _sender.Send(new SelectBarberCommand(id));
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
        [HttpPost("setappointment")]
        public async Task<IActionResult> SetAppointment(SetAppointmentCommand command)
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

        [HttpPost("getavailabletime")]
        [Authorize(Roles = "barber,user")]
        public async Task<IActionResult> GetAvailableTime(string barberId, DateTime date)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = await _sender.Send(new BarberFreeTimeQuery(barberId, date));
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
