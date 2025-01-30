using ECommerce_API.Core.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECommerce_API.Infrastructure
{
    public static class ServiceExtensions
    {
        public static void AddIdentityInfrastructure(this IServiceCollection services)
        {
            // 🔹 Identity konfigurieren
            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
            })
                .AddEntityFrameworkStores<ECommerceDbContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ECommerceDbContext, Guid>>()
                .AddRoleStore<RoleStore<ApplicationRole, ECommerceDbContext, Guid>>();
        }

        public static async Task InitializeRolesAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            await IdentitySeeder.SeedRolesAsync(roleManager);
        }

    }
}
