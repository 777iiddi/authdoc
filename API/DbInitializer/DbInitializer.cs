using System;
using System.Linq;
using API.Data;
using API.DbInistializer;
using API.models;
using API.models.ViewModels;
using API.Utility;                   // your Helper class lives here
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace API.DbInistializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly DataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(
            DataContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            // 1️⃣ Appliquer les migrations pendantes
            if (_context.Database.GetPendingMigrations().Any())
            {
                Console.WriteLine("⏳ Applying migrations...");
                _context.Database.Migrate();
            }

            // 2️⃣ Seed des rôles
            string[] roles = { Helper.Admin, Helper.Doctor, Helper.user };
            foreach (var role in roles)
            {
                var exists = _roleManager.RoleExistsAsync(role).GetAwaiter().GetResult();
                if (!exists)
                {
                    Console.WriteLine($"➕ Creating role '{role}'");
                    _roleManager.CreateAsync(new IdentityRole(role))
                                .GetAwaiter().GetResult();
                }
            }

            // 3️⃣ Seed de l’utilisateur admin
            const string adminEmail = "admin@gmail.com";
            const string adminPassword = "Admin@123!";  // respecte la policy par défaut

            var admin = _userManager.FindByEmailAsync(adminEmail)
                                    .GetAwaiter().GetResult();
            if (admin == null)
            {
                Console.WriteLine("➕ Creating admin user");

                admin = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    Name = "Admin mehdi"
                };

                var createResult = _userManager
                    .CreateAsync(admin, adminPassword)
                    .GetAwaiter().GetResult();

                if (!createResult.Succeeded)
                {
                    Console.WriteLine("❌ Failed to create admin:");
                    foreach (var err in createResult.Errors)
                        Console.WriteLine($"   {err.Code}: {err.Description}");
                    return;
                }

                // Ajoute l’admin au rôle Admin
                _userManager
                    .AddToRoleAsync(admin, Helper.Admin)
                    .GetAwaiter().GetResult();

                Console.WriteLine("✅ Admin user seeded successfully");
            }
            else
            {
                Console.WriteLine("ℹ️ Admin user already exists");
            }
        }
    }

  
}
