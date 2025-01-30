using ECommerce_API.Application.DTO.Identity;
using ECommerce_API.Core.Identity;
using Microsoft.AspNetCore.Identity;

namespace ECommerce_API.Application.Services
{
    public class AccountService
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountService
            (SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> RegisterUser(RegisterDTO registerDTO)
        {
            var user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                UserName = registerDTO.Email,
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if(result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Customer");

                await _signInManager.SignInAsync(user, isPersistent : false);
            }

            return result;
        }
    }
}
