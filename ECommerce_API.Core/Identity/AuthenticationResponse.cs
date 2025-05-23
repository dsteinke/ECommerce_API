﻿namespace ECommerce.Domain.Identity
{
    public class AuthenticationResponse
    {
        public string? Email { get; set; } = string.Empty;
        public string? Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}
