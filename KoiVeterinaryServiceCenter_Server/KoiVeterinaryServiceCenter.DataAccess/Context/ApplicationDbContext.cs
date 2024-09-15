using KoiVeterinaryServiceCenter.Model.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiVeterinaryServiceCenter.DataAccess.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Doctor> Doctors { get; set; } 
        public DbSet<DoctorRating> DoctorRatings { get; set; } 
        public DbSet<DoctorSchedules> DoctorSchedules { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetDisease> PetsDiseases { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<RefreshTokens> RefreshTokens { get; set; }
    }
}
