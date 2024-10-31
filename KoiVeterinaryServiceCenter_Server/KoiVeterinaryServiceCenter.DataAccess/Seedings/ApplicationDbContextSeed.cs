using KoiVeterinaryServiceCenter.Models.Domain;
using KoiVeterinaryServiceCenter.Utility.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KoiVeterinaryServiceCenter.DataAccess.Seedings;

public class ApplicationDbContextSeed
{
    public static void SeedEmailTemplate(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EmailTemplate>().HasData(
            new
            {
                Id = Guid.NewGuid(),
                TemplateName = "Appointment booked",
                SenderName = "Koi Veterinary Service Center",
                SenderEmail = "koiveterinaryservice@gmail.com",
                Category = "Appointment",
                SubjectLine = "Welcome to Koi Veterinary Service Center!",
                PreHeaderText = "Thank you for signing up!",
                PersonalizationTags = "{FirstName}, {LastName}",
                BodyContent =
                    "Dear [UserFullName],<br><br>Welcome to Koi Veterinary Service Center! We are thrilled to have you as part of our community dedicated to the care and well-being of your beloved pets.",
                FooterContent = "<p>Contact us at koiveterinaryservice@gmail.com</p>",
                CallToAction = "<a href=\"{{VerificationLink}}\">Verify Your Email</a>",
                Language = "English",
                RecipientType = "Customer",
                CreateBy = "System",
                CreateTime = DateTime.Now,
                UpdateBy = "Admin",
                UpdateTime = DateTime.Now,
                Status = 1
            },
            new
            {
                Id = Guid.NewGuid(),
                TemplateName = "ForgotPasswordEmail",
                SenderName = "Koi Veterinary Service Center",
                SenderEmail = "koiveterinaryservice@gmail.com",
                Category = "Security",
                SubjectLine = "Reset Your Password",
                PreHeaderText = "Reset your password to regain access",
                PersonalizationTags = "[UserFullName], [ResetPasswordLink]",
                BodyContent =
                    "Hi [UserFullName],<br><br>We received a request to reset your password. Click the link below to reset your password.",
                FooterContent = "If you did not request a password reset, please ignore this email.",
                CallToAction =
                    $"https://cursuslms.xyz/sign-in/verify-email?userId=user.Id&token=Uri.EscapeDataString(token)",
                Language = "English",
                RecipientType = "Customer",
                CreateBy = "System",
                CreateTime = DateTime.Now,
                UpdateBy = "Admin",
                UpdateTime = DateTime.Now,
                Status = 1
            },
            new
            {
                Id = Guid.NewGuid(),
                TemplateName = "SendVerifyEmail",
                SenderName = "Koi Veterinary Service Center",
                SenderEmail = "koiveterinaryservice@gmail.com",
                Category = "Verify",
                SubjectLine = "Cursus Verify Email",
                PreHeaderText = "User Account Verified!",
                PersonalizationTags = "{FirstName}, {LinkLogin}",
                BodyContent =
                    "<p>Thank you for registering your Cursus account. Click here to go back the page</p>",
                FooterContent = "<p>Contact us at cursusservicetts@gmail.com</p>",
                CallToAction = "<a href=\"{{Login}}\">Login now</a>",
                Language = "English",
                RecipientType = "Customer",
                CreateBy = "System",
                CreateTime = DateTime.Now,
                UpdateBy = "Admin",
                UpdateTime = DateTime.Now,
                Status = 1
            }
        );
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="modelBuilder"></param>
    public static void SeedAdminAccount(ModelBuilder modelBuilder)
    {
        var customerRoleId = "8fa7c7bb-b4dc-480d-a660-e07a90855d5d";
        var doctorRoleId = "35446074-daa5-4973-bf02-82301a5eb327";
        var adminRoleId = "8fa7c7bb-daa5-a660-bf02-82301a5eb327";
        var staffRoleId = "8sa7c7bb-b4dc-480d-a660-82301a5eb327";

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
            },
            new IdentityRole
            {
                Id = staffRoleId,
                ConcurrencyStamp = StaticUserRoles.Staff,
                Name = StaticUserRoles.Staff,
                NormalizedName = StaticUserRoles.Staff,
            }
        };

        modelBuilder.Entity<IdentityRole>().HasData(roles);

        // Seeding admin user
        var adminUserId = "AdminId";
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
        
        // Seeding staff user
        var staffUserId = "StaffId";
        var staffUser = new ApplicationUser
        {
            Id = staffUserId,
            Gender = "Male", // Set appropriate value
            FullName = "Staff_1 User",
            BirthDate = new DateTime(1990, 1, 1), // Set appropriate value
            AvatarUrl = "https://example.com/avatarStaff.png", // Set appropriate value
            Country = "Country", // Set appropriate value
            Address = "123 Staff St",
            UserName = "staff1@gmail.com",
            NormalizedUserName = "STAFF1@GMAIL.COM",
            Email = "staff1@gmail.com",
            NormalizedEmail = "STAFF1@GMAIL.COM",
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null, "Staff123!"),
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            PhoneNumber = "0123456789",
            PhoneNumberConfirmed = true,
            TwoFactorEnabled = false,
            LockoutEnd = null,
            LockoutEnabled = true,
            AccessFailedCount = 0
        };
        
        // Seeding second staff user
        var staffUserId2 = "StaffId2";
        var staffUser2 = new ApplicationUser
        {
            Id = staffUserId2,
            Gender = "Female",
            FullName = "Staff_2 User",
            BirthDate = new DateTime(1991, 2, 2),
            AvatarUrl = "https://example.com/avatarStaff2.png",
            Country = "Country",
            Address = "456 Staff St",
            UserName = "staff2@gmail.com",
            NormalizedUserName = "STAFF2@GMAIL.COM",
            Email = "staff2@gmail.com",
            NormalizedEmail = "STAFF2@GMAIL.COM",
            EmailConfirmed = true,
            PasswordHash = hasher.HashPassword(null, "Staff123!"),
            SecurityStamp = Guid.NewGuid().ToString(),
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            PhoneNumber = "0987654321",
            PhoneNumberConfirmed = true,
            TwoFactorEnabled = false,
            LockoutEnd = null,
            LockoutEnabled = true,
            AccessFailedCount = 0
        };
        
        modelBuilder.Entity<ApplicationUser>().HasData(adminUser, staffUser,staffUser2);

        // Assigning the admin role to the admin user
        modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = adminRoleId,
            UserId = adminUserId
        });
    }
}