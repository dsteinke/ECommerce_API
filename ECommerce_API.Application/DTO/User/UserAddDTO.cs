using System.ComponentModel.DataAnnotations;

namespace ECommerce_API.Application
{
    public class UserAddDTO
    {
        [Required]
        public string Username { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string PasswordHash { get; set; } = null!;

        public ECommerce_API.Core.User ToUser()
        {
            return new ECommerce_API.Core.User
            {
                UserId = Guid.NewGuid(),
                Username = Username,
                Email = Email,
                PasswordHash = PasswordHash,
            };
        }
    }
}
