using KoiVeterinaryServiceCenter.DataAccess.Seedings;
using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Models.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KoiVeterinaryServiceCenter.DataAccess.Context;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<ApplicationUser> ApplicationUsers { get; set; }

    

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<DoctorRating> DoctorRatings { get; set; }
    public DbSet<DoctorSchedules> DoctorSchedules { get; set; }
    public DbSet<DoctorServices> DoctorServices { get; set; }

    public DbSet<Post> Posts { get; set; }

    public DbSet<Service> Services { get; set; }

    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Slot> Slots { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

   

    public DbSet<EmailTemplate> EmailTemplates { get; set; }
    public DbSet<PaymentTransactions> PaymentTransactions { get; set; }
    public DbSet<Pet> Pets { get; set; }
    public DbSet<Disease> Diseases { get; set; }
    public DbSet<PetDisease> PetDiseases { get; set; }
    public DbSet<PetService> PetServices { get; set; }
    public DbSet<AppointmentPet> AppointmentPets { get; set; }
    public DbSet<Pool> Pool { get; set; }
 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Seed data
        ApplicationDbContextSeed.SeedAdminAccount(modelBuilder);

        //Seed Email Template
        ApplicationDbContextSeed.SeedEmailTemplate(modelBuilder);

        // Thiết lập khóa chính hỗn hợp cho bảng trung gian DoctorService
        modelBuilder.Entity<DoctorServices>()
            .HasKey(ds => new { ds.DoctorId, ds.ServiceId });
        // Thiết lập quan hệ với Doctor
        modelBuilder.Entity<DoctorServices>()
            .HasOne(ds => ds.Doctor)
            .WithMany(d => d.DoctorServices)
            .HasForeignKey(ds => ds.DoctorId);
        // Thiết lập quan hệ với Service
        modelBuilder.Entity<DoctorServices>()
            .HasOne(ds => ds.Service)
            .WithMany(s => s.DoctorServices)
            .HasForeignKey(ds => ds.ServiceId);
        
        // Cấu hình index trên cột AppointmentNumber và chỉ định tính duy nhất
        modelBuilder.Entity<Appointment>()
            .HasIndex(a => a.AppointmentNumber)
            .IsUnique();

        // Cấu hình quan hệ giữa PaymentTransactions và Appointment qua AppointmentNumber
        modelBuilder.Entity<PaymentTransactions>()
            .HasOne(pt => pt.Appointment)
            .WithMany()
            .HasForeignKey(pt => pt.AppointmentNumber)
            .HasPrincipalKey(a => a.AppointmentNumber)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Thiết lập khóa chính hỗn hợp cho bảng trung gian DoctorService
        modelBuilder.Entity<PetDisease>()
            .HasKey(pd => new { pd.PetId, pd.DiseaseId });
        // Thiết lập quan hệ với Pet
        modelBuilder.Entity<PetDisease>()
            .HasOne(pd => pd.Pet)
            .WithMany(p => p.PetDiseases)
            .HasForeignKey(pd => pd.PetId);
        // Thiết lập quan hệ với Disease
        modelBuilder.Entity<PetDisease>()
            .HasOne(pd => pd.Disease)
            .WithMany(d => d.PetDiseases)
            .HasForeignKey(pd => pd.DiseaseId);
        
        //Thiết lập khóa chính hỗn hợp cho bảng trung gian PetService
        modelBuilder.Entity<PetService>()
            .HasKey(ps => new { ps.PetId, ps.ServiceId });
        //Thiết lập với bảng trung gian PetService
        modelBuilder.Entity<PetService>()
            .HasOne(ps => ps.Pet)
            .WithMany(p => p.PetServices)
            .HasForeignKey(ps => ps.PetId);
        // Thiết lập quan hệ với PetService
        modelBuilder.Entity<PetService>()
            .HasOne(ps => ps.Service)
            .WithMany(s => s.PetServices)
            .HasForeignKey(ps => ps.ServiceId);
        
        // Thiết lập khóa chính hỗn hợp cho bảng trung gian AppointmentPet
        modelBuilder.Entity<AppointmentPet>()
            .HasKey(ap => new { ap.AppointmentId, ap.PetId });
        // Thiết lập quan hệ với Appointment
        modelBuilder.Entity<AppointmentPet>()
            .HasOne(ap => ap.Appointment)
            .WithMany(a => a.AppointmentPets)
            .HasForeignKey(ap => ap.AppointmentId)
            .OnDelete(DeleteBehavior.NoAction);
        // Thiết lập quan hệ với Pet
        modelBuilder.Entity<AppointmentPet>()
            .HasOne(ap => ap.Pet)
            .WithMany(p => p.AppointmentPets)
            .HasForeignKey(ap => ap.PetId)
            .OnDelete(DeleteBehavior.NoAction);
        
    }
}