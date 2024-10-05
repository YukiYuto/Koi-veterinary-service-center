using KoiVeterinaryServiceCenter.Model.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KoiVeterinaryServiceCenter.DataAccess.Seeding;

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
        public DbSet<DoctorService> DoctorServices { get; set; }

        public DbSet<Pet> Pets { get; set; }
        public DbSet<PetDisease> PetsDiseases { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<RefreshTokens> RefreshTokens { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<AppointmentStatus> AppointmentStatuses { get; set; }
        public DbSet<Slot> Slots { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        
        public DbSet<EmailTemplate> EmailTemplates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data
            ApplicationDbContextSeed.SeedAdminAccount(modelBuilder);
            
            //Seed Email Template
            ApplicationDbContextSeed.SeedEmailTemplate(modelBuilder);

            // Thiết lập khóa chính hỗn hợp cho bảng trung gian DoctorService
            modelBuilder.Entity<DoctorService>()
                .HasKey(ds => new { ds.DoctorId, ds.ServiceId });

            // Thiết lập quan hệ với Doctor
            modelBuilder.Entity<DoctorService>()
                .HasOne(ds => ds.Doctor)
                .WithMany(d => d.DoctorServices)
                .HasForeignKey(ds => ds.DoctorId);

            // Thiết lập quan hệ với Service
            modelBuilder.Entity<DoctorService>()
                .HasOne(ds => ds.Service)
                .WithMany(s => s.DoctorServices)
                .HasForeignKey(ds => ds.ServiceId);
        }
    }
}