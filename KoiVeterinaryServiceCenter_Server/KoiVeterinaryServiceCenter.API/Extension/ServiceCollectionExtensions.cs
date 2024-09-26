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
            return services;
        }
    }
}