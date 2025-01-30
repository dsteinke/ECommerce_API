using System.ComponentModel.DataAnnotations;

namespace ECommerce_API.Application
{
    public class LoginUserDTO
    {
        [Required]
        public string UsernameEmail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
