namespace Interfaces.DTO.User
{
    public class UserAddDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

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
