﻿namespace Interfaces.DTO.User
{
    public class UserResponseDTO
    {
        public Guid UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
