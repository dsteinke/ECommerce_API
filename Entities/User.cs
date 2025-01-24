namespace Entities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        // Navigation Properties
        public Cart Cart { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
