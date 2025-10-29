using ClinicManagementSystem.Models;
using ClinicManagementSystem.Models.Data;
using Microsoft.AspNetCore.Identity;

namespace ClinicManagementSystem.Services
{
    public static class RegisterationService
    {
        public static IServiceCollection AddIdentityRegisterationService(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true; // At least one digit
                options.Password.RequireLowercase = true; // At least one lowercase letter
                options.Password.RequireUppercase = true; // At least one uppercase letter
                options.Password.RequiredUniqueChars = 1; // At least one unique character
                options.Password.RequiredLength = 6; // Minimum length of 6 characters

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); // Lockout duration
                options.Lockout.MaxFailedAccessAttempts = 5; // Max failed access attempts
                options.Lockout.AllowedForNewUsers = true; // Enable lockout for new users

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+"; // Allowed characters in username
                options.User.RequireUniqueEmail = true; // Require unique email

                // SignIn settings.
                options.SignIn.RequireConfirmedEmail = false; // Email confirmation not required for sign-in


            }).AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();


            return services;
        }
    }
}
