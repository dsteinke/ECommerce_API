namespace ECommerce_API.Core
{
    public class UserRole
    {
        public Guid UserId { get; set; } //Foreign key to User
        public User User { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
