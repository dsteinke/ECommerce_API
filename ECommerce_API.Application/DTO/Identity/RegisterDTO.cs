using System.ComponentModel.DataAnnotations;

namespace ECommerce.Application.DTO.Identity
{
    public class RegisterDTO
    {
        [Required]
        public string PersonName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        //[Remote(action: "IsEmailAlreadyRegistered", controller: "Account", ErrorMessage = "Email is already used.")]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
