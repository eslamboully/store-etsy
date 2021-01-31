using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure.Data;

namespace WEB.Areas.Dashboard.Extensions
{
    public static class DatabaseExtension
    {
        public static IServiceCollection AddDatabaseExtension(this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<DatabaseContext>(configs =>
            {
                configs.UseSqlServer(config.GetConnectionString("DefaultString"));
            });

            return services;
        }
    }
}