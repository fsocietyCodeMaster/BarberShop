using BarberShop.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BarberShop.Customized.SeedData
{
    public class SeedingData
    {
        private readonly UserManager<T_User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<SeedingData> _logger;
        private readonly IConfiguration _configuration;
        public SeedingData(UserManager<T_User> userManager, RoleManager<IdentityRole> roleManager, ILogger<SeedingData> logger, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
            _configuration = configuration;

        }
        public async Task SeedAdmins()
        {
        }
    }
}
