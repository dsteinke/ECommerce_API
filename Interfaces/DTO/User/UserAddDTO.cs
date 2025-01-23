using System.ComponentModel.DataAnnotations;

namespace Interfaces.DTO.User
{
    public class UserAddDTO
    {
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string PasswordHash { get; set; } = null!;

        public Entities.User ToUser()
        {
            return new Entities.User
            {
                UserId = Guid.NewGuid(),
                Username = Username,
                Email = Email,
                PasswordHash = PasswordHash,
                Role = "Customer"
            };
        }
    }
}
