using KoiVeterinaryServiceCenter.DataAccess.IRepository;
using KoiVeterinaryServiceCenter.DataAccess.Repository;
using KoiVeterinaryServiceCenter.Services.IServices;
using KoiVeterinaryServiceCenter.Services.Services;
using StackExchange.Redis;

namespace KoiVeterinaryServiceCenter.API.Extension;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services,
        ConfigurationManager builderConfiguration)
    {
        // Đọc chuỗi kết nối Redis từ file cấu hình
        var redisConnectionString = builderConfiguration.GetValue<string>("Redis:ConnectionString");

        // Đăng ký IConnectionMultiplexer
        var connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
        services.AddSingleton<IConnectionMultiplexer>(connectionMultiplexer);
        
        // Registering IUnitOfWork with its implementation UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        // Registering IAuthService with its implementation AuthService
        services.AddScoped<IAuthService, AuthService>();
        // Registering IDoctorService with its implementation DoctorService
        services.AddScoped<ITokenService, TokenService>();
        // Registering IDoctorSchedulesService with its implementation DoctorService
        services.AddScoped<IDoctorSchedulesService, DoctorSchedulesService>();
        // Registering ISlotService with its implementation SlotService
        services.AddScoped<ISlotService, SlotService>();
        // Registering IAppointmentService with its implementation AppointmentService
        services.AddScoped<IAppointmentService, AppointmentService>();
        // Registering IDoctorService its implementation DoctorService
        services.AddScoped<IDoctorService, DoctorService>();
        // Registering IEmailService with its implementation EmailService
        services.AddScoped<IEmailService, EmailService>();
        // Registering IServiceService with its implementation ServiceService
        services.AddScoped<IServicesService, ServiceService>();
        // Registering IDoctorService with its implementation DoctorService
        services.AddScoped<IDoctorServicesService, DoctorServicesService>();
        // Registering IRedisService with its implementation RedisService
        services.AddScoped<IRedisService, RedisService>();
        // Registering IPaymentService with its implementation PaymentService
        services.AddScoped<IPaymentService, PaymentService>();
        // Registering IPostService with its implementation PostService
        services.AddScoped<IPostService, PostService>();
        //registing Pet
        services.AddScoped<IPetService, PetsService>();
        // Resgistering IDoctorRatingService with its implementation DoctorRatingService
        services.AddScoped<IDoctorRatingService, DoctorRatingService>();
        // Resgistering ITransactionsService with its implementation TransactionsService
        services.AddScoped<ITransactionsService, TransactionsService>();
        // Resgistering IPoolService with its implementation PoolService
        services.AddScoped<IPoolService, PoolService>();
        // Resgistering IPoolService with its implementation PoolService
        services.AddScoped<IPetServiceService, PetServiceService>();
        services.AddScoped<IAppointmentDepositService, AppointmentDepositService>();
        return services;
    }
}