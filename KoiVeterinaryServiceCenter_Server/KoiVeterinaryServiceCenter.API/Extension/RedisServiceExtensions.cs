using KoiVeterinaryServiceCenter.Utility.Constants;
using StackExchange.Redis;

namespace KoiVeterinaryServiceCenter.API.Extension
{
    public static class RedisServiceExtensions
    {
        public static WebApplicationBuilder AddRedisCache(this WebApplicationBuilder builder)
        {
            string connectionString =
                builder.Configuration.GetSection("Redis")[StaticConnectionString.REDIS_ConnectionString];
            builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(connectionString));
            return builder;
        }
    }
}
