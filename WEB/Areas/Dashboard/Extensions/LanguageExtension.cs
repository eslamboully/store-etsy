using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;

namespace WEB.Areas.Dashboard.Extensions
{
    public static class LanguageExtension
    {
        public static IServiceCollection AddLanguageExtension(this IServiceCollection services)
        {
            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("ar"),
                new CultureInfo("en")
            };
            services.AddRequestLocalization(options =>
            {
                options.DefaultRequestCulture = new RequestCulture("ar");
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
                options.RequestCultureProviders = new List<IRequestCultureProvider>()
                {
                    new CookieRequestCultureProvider { CookieName = "AdminLocale"}
                };
            });
            services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix);
            
            return services;
        }
    }
}