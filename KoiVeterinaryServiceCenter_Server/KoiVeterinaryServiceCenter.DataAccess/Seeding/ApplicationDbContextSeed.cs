using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Utility.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KoiVeterinaryServiceCenter.DataAccess.Seeding
{
    public class ApplicationDbContextSeed
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void SeedAdminAccount(ModelBuilder modelBuilder)
        {
            var customerRoleId = "8fa7c7bb-b4dc-480d-a660-e07a90855d5d";
            var doctorRoleId = "35446074-daa5-4973-bf02-82301a5eb327";
            var adminRoleId = "8fa7c7bb-daa5-a660-bf02-82301a5eb327"; // Add admin role

            var roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Id = customerRoleId,
                ConcurrencyStamp = StaticUserRoles.Customer,
                Name = StaticUserRoles.Customer,
                NormalizedName = StaticUserRoles.Customer,
            },
            new IdentityRole
            {
                Id = doctorRoleId,
                ConcurrencyStamp = StaticUserRoles.Doctor,
                Name = StaticUserRoles.Doctor,
                NormalizedName = StaticUserRoles.Doctor,
            },
            new IdentityRole
            {
                Id = adminRoleId,
                ConcurrencyStamp = StaticUserRoles.Admin,
                Name = StaticUserRoles.Admin,
                NormalizedName = StaticUserRoles.Admin,
            }
        };

            modelBuilder.Entity<IdentityRole>().HasData(roles);

            // Seeding admin user
            var adminUserId = "BestZedAndYasuo";
            var hasher = new PasswordHasher<ApplicationUser>();
            var adminUser = new ApplicationUser
            {
                Id = adminUserId,
                Gender = "Male", // Set appropriate value
                FullName = "Admin User",
                BirthDate = new DateTime(1990, 1, 1), // Set appropriate value
                AvatarUrl = "https://example.com/avatar.png", // Set appropriate value
                Country = "Country", // Set appropriate value
                Address = "123 Admin St",
                UserName = "admin@gmail.com",
                NormalizedUserName = "ADMIN@GMAIL.COM",
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin123!"),
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                PhoneNumber = "1234567890",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnd = null,
                LockoutEnabled = true,
                AccessFailedCount = 0
            };

            modelBuilder.Entity<ApplicationUser>().HasData(adminUser);

            // Assigning the admin role to the admin user
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminUserId
            });
        }
    }
}
