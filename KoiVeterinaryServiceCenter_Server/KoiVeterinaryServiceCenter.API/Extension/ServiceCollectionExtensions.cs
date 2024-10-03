using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.DataAccess.Repository;
using KoiVeterinaryServiceCenter.Services.IServices;
using KoiVeterinaryServiceCenter.Services.Services;

namespace KoiVeterinaryServiceCenter.API.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            // Registering IUnitOfWork with its implementation UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            // Registering IAuthService with its implementation AuthService
            services.AddScoped<IAuthService, AuthService>();
            // Registering IDoctorService with its implementation DoctorService
            services.AddScoped<ITokenService, TokenService>();
            // Registering IUserManagerRepository its implementation UserManagerRepository
            services.AddScoped<IUserManagerRepository, UserManagerRepository>();
            // Registering ISlotService with its implementation SlotService
            services.AddScoped<ISlotService, SlotService>();
            // Registering IAppointmentService with its implementation AppointmentService
            services.AddScoped<IAppointmentService, AppointmentService>();

            services.AddScoped<IPetRepository, PetRepository>();

            services.AddScoped<IPetService, PetService>();

            services.AddScoped<IDiseaseRepository, DiseaseRepository>();

            services.AddScoped<IDiseaseService, DiseaseService>();

            services.AddScoped<IPetDiseaseRepository, PetDiseaseRepository>();

            services.AddScoped<IPetDiseaseService, PetDiseaseService>();
            // Registering IUserManagerRepository its implementation DoctorService
            services.AddScoped<IDoctorService, DoctorService>(); 
            // Registering IEmailService with its implementation EmailService
            services.AddScoped<IEmailService, EmailService>();
            // Registering IServiceService with its implementation ServiceService
            services.AddScoped<IServiceService, ServiceService>();
            return services;
        }
    }
}