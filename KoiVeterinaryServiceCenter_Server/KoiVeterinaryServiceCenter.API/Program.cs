using KoiVeterinaryServiceCenter.API.Extension;
using KoiVeterinaryServiceCenter.DataAccess.Context;
using KoiVeterinaryServiceCenter.Model.Domain;
using KoiVeterinaryServiceCenter.Services.Mappings;
using KoiVeterinaryServiceCenter.Utility.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KoiVeterinaryServiceCenter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Configure DbContext with SQL Server
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString(StaticConnectionString.SQLDB_DefaultConnection));
            });

            // Register AutoMapper
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

            // Register services life cycle
            builder.Services.RegisterServices(); // Ensure services are registered correctly

            // Configure Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Configure authentication and authorization
            builder.Services.AddAuthentication(); // This is optional and depends on your setup
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            ApplyMigration(app);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication(); // Ensure authentication middleware is added
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void ApplyMigration(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}