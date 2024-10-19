using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.DataAccess.Repository;
using KoiVeterinaryServiceCenter.Services.IServices;
using KoiVeterinaryServiceCenter.Services.Services;
using StackExchange.Redis;

namespace KoiVeterinaryServiceCenter.API.Extension
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, ConfigurationManager builderConfiguration)
        {
            // Đọc chuỗi kết nối Redis từ file cấu hình
        var redisConnectionString = builderConfiguration.GetValue<string>("Redis:ConnectionString");

            // Sử dụng ConfigurationOptions để cấu hình Redis, thêm AbortOnConnectFail = false để tránh crash khi không thể kết nối ban đầu
            var redisConfigOptions = ConfigurationOptions.Parse(redisConnectionString);
            redisConfigOptions.AbortOnConnectFail = false;

            // Đăng ký IConnectionMultiplexer với cấu hình Redis
            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                return ConnectionMultiplexer.Connect(redisConfigOptions);
            });

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
            // Registering IUserManagerRepository its implementation DoctorService
            services.AddScoped<IDoctorService, DoctorService>(); 
            // Registering IEmailService with its implementation EmailService
            services.AddScoped<IEmailService, EmailService>();
            // Registering IDoctorSchedulesService with its implementation DoctorSchedulesService
            services.AddScoped<IDoctorSchedulesService, DoctorSchedulesService>();
            // Registering IServiceService with its implementation ServiceService
            services.AddScoped<IServicesService, ServiceService>();
            // Registering IDoctorService with its implementation DoctorService
            services.AddScoped<IDoctorServicesService, DoctorServicesService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IRedisService, RedisService>();

            return services;
        }
    }
}