using Microsoft.Extensions.DependencyInjection;

namespace WEB.Areas.Dashboard.Extensions
{
    public static class AuthenticationExtension
    {   
        public static IServiceCollection AddAuthenticationExtension(this IServiceCollection services)
        {
            services.AddAuthentication()
                .AddCookie("admin",options =>
                {
                    options.LoginPath = "/dashboard/login";
                    options.LogoutPath = "/dashboard/logout";
                    options.AccessDeniedPath = "/dashboard/error";
                })
                .AddCookie("web",options =>
                {
                    options.LoginPath = "/login";
                    options.LogoutPath = "/logout";
                    options.AccessDeniedPath = "/error";
                });

            return services;
        }
    }
}