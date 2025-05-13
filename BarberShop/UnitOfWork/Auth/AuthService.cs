using BarberShop.DTO.ResponseResult;
using BarberShop.Model;
using BarberShop.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BarberShop.UnitOfWork.User
{
    public class AuthService : IAuthentication
    {
        private readonly UserManager<T_User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthService(UserManager<T_User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<ResponseDTO> RegisterAsync(string UserName, string Password, string FullName, string? Bio, TimeSpan? StartTime, TimeSpan? EndTime, string PhoneNumber, string role)
        {
            var identityUser = new T_User()
            {
                UserName = UserName,
                FullName = FullName,
                PhoneNumber = PhoneNumber,
                IsActive = true
            };
            //if (ImageUrl != null)
            //{
            //    if (ImageUrl.Length == 0)
            //    {
            //        var error = new ResponseDTO
            //        {
            //            Message = "فایل آپلود شده خالی است",
            //            IsSuccess = false,
            //            StatusCode = StatusCodes.Status400BadRequest,
            //            Data = null
            //        };
            //        return error;
            //    }
            //    if (ImageUrl.Length > 1048576)
            //    {
            //        var error = new ResponseDTO
            //        {
            //            Message = "حجم فایل تصویر بیشتر از ۱ مگابایت است",
            //            IsSuccess = false,
            //            StatusCode = StatusCodes.Status400BadRequest,
            //            Data = null
            //        };
            //        return error;
            //    }
            //    var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            //    var fileExtension = Path.GetExtension(ImageUrl.FileName);
            //    if (!allowedExtensions.Contains(fileExtension.ToLower()))
            //    {
            //        return new ResponseDTO
            //        {
            //            Message = "نوع فایل نامعتبر است",
            //            IsSuccess = false,
            //            StatusCode = StatusCodes.Status400BadRequest,
            //            Data = null
            //        };
            //    }
            //    var uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            //    if (!Directory.Exists(uploadFolder))
            //    {
            //        Directory.CreateDirectory(uploadFolder);
            //    }
            //    var fileName = Guid.NewGuid().ToString() + fileExtension;
            //    var url = Path.Combine(uploadFolder, fileName);
            //    using (var fileStream = new FileStream(url, FileMode.Create))
            //    {
            //        await ImageUrl.CopyToAsync(fileStream);
            //    }
            //    identityUser.ImageUrl = url;

            if (role == "user")
            {

                if (!await _roleManager.RoleExistsAsync("user"))
                {
                    var userRole = new IdentityRole()
                    {
                        Name = "user",
                    };
                    await _roleManager.CreateAsync(userRole);

                }
                var result = await _userManager.CreateAsync(identityUser, Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(identityUser, "user");
                    return new ResponseDTO()
                    {
                        Message = "کاربر با موفقیت ایجاد شد",
                        IsSuccess = true,
                        StatusCode = StatusCodes.Status200OK,
                        Data = new {role}
                    };
                }
                else
                {
                    return new ResponseDTO()
                    {
                        Message = "مشکلی در ایجاد کاربر رخ داده است",
                        IsSuccess = true,
                        StatusCode = StatusCodes.Status200OK,
                        Data = null
                    };
                }

            }
            else if (role == "barber")
            {
                if (!await _roleManager.RoleExistsAsync("barber"))
                {
                    var userRole = new IdentityRole()
                    {
                        Name = "barber",
                    };
                    await _roleManager.CreateAsync(userRole);

                }
                identityUser.Bio = Bio;
                identityUser.StartTime = StartTime;
                identityUser.EndTime = EndTime;
                var result = await _userManager.CreateAsync(identityUser, Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(identityUser, "barber");
                    return new ResponseDTO()
                    {
                        Message = "کاربر با موفقیت ایجاد شد",
                        IsSuccess = true,
                        StatusCode = StatusCodes.Status200OK,
                        Data = new { role }
                    };
                }
                else
                {
                    return new ResponseDTO()
                    {
                        Message = "مشکلی در ایجاد کاربر رخ داده است",
                        IsSuccess = true,
                        StatusCode = StatusCodes.Status200OK,
                        Data = null
                    };
                }
            }
            else if (role == "barbershop")
            {
                if (!await _roleManager.RoleExistsAsync("barbershop"))
                {
                    var userRole = new IdentityRole()
                    {
                        Name = "barbershop",
                    };
                    await _roleManager.CreateAsync(userRole);

                }
                var result = await _userManager.CreateAsync(identityUser, Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(identityUser, "barbershop");
                    return new ResponseDTO()
                    {
                        Message = "کاربر با موفقیت ایجاد شد",
                        IsSuccess = true,
                        StatusCode = StatusCodes.Status200OK,
                        Data = new { role }
                    };
                }
                else
                {
                    return new ResponseDTO()
                    {
                        Message = "مشکلی در ایجاد کاربر رخ داده است",
                        IsSuccess = true,
                        StatusCode = StatusCodes.Status200OK,
                        Data = null
                    };
                }

            }
            else
            {
                return new ResponseDTO()
                {
                    Message = "There is problem while sending the data.",
                    IsSuccess = true,
                    StatusCode = StatusCodes.Status200OK,
                    Data = null
                };
            }
        }
        //else
        //{
        //    return new ResponseDTO()
        //    {
        //        Message = "There is no image.",
        //        IsSuccess = true,
        //        StatusCode = StatusCodes.Status200OK,
        //        Data = null
        //    };
        //}


        public async Task<ResponseDTO> LoginAsync(string UserName, string Password)
        {
            var userExist = await _userManager.Users.FirstOrDefaultAsync(c => c.UserName == UserName);
            if (userExist == null)
            {
                return new ResponseDTO
                {
                    Message = "یوز یا رمز عبور اشتباه است لطفا مجددا تلاش کنید",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Data = null
                };

            }
            var result = await _userManager.CheckPasswordAsync(userExist, Password);
            if (!result)
            {
                return new ResponseDTO
                {
                    Message = "یوزر یا رمز عبور اشتباه است لطفا مجددا تلاش کنید",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                    Data = null
                };

            }
            var userRole = await _userManager.GetRolesAsync(userExist);

            var SecreteKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var signingKey = new SigningCredentials(SecreteKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier,userExist.Id),
                new(ClaimTypes.Name, userExist.FullName)
            };
            if (userRole != null)
            {
                foreach (var role in userRole)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }
            var jwtConfig = new JwtSecurityToken(
               issuer: _configuration["Jwt:Issuer"],
               audience: _configuration["Jwt:Audience"],
               claims: claims,
               notBefore: DateTime.UtcNow,
               expires: DateTime.Now.AddMonths(3),
               signingCredentials: signingKey

               );
            var token = new JwtSecurityTokenHandler().WriteToken(jwtConfig);
            return new ResponseDTO()
            {
                Message = token,
                IsSuccess = true,
                //Role = userRole.FirstOrDefault(),
                StatusCode = StatusCodes.Status200OK,
                Data = new {UserRole = userRole}
            };
        }
        public ResponseDTO GetRoles()
        {
            var roles = _roleManager.Roles.Select(c => c.Name).ToList();
            var success = new ResponseDTO
            {
                Message = "رول ها با موفقیت ارسال شد",
                IsSuccess = true,
                StatusCode = StatusCodes.Status200OK,
                Data = roles
            };
            return success;
        }
    }
}
