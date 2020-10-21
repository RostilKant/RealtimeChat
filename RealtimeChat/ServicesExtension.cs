using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RealtimeChat
{
    public static class ServicesExtension
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
                options.AddPolicy("CorsPolicy", builder => 
                    builder
                        .AllowCredentials()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("http://localhost:4200")
                ));
        }

        public static void ConfigureSignalR(this IServiceCollection services)
        {
            services.AddSignalR(hubOptions => {
                hubOptions.EnableDetailedErrors = true;
            });
        }

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<ApplicationContext>(builder =>
            {
                builder.UseNpgsql(configuration.GetConnectionString("sqlConnection"));
            } );
        
        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
            });
            
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();
        }
        
    }
}