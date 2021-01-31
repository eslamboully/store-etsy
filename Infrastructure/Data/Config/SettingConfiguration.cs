using System.Collections.Generic;
using Core.Areas.Dashboard.Entities;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Config
{
    public static class SettingSeeder
    {
        public static async Task SettingSeedData(DatabaseContext context)
        {
            if (!context.Set<Setting>().Any())
            {
                var site_name = new Setting
                {
                    Type = 1,
                    Var = "site_name",
                    IsStatic = false,
                    Translations = new List<SettingTranslation>()
                    {
                        new SettingTranslation
                        {
                            Locale = "ar",
                            Value = "اسم الموقع",
                            DisplayName = "اسم الموقع",
                        },
                        new SettingTranslation
                        {
                            Locale = "en",
                            Value = "site name",
                            DisplayName = "Site Name",
                        },
                    }
                };
                var logo = new Setting
                {
                    Type = 2,
                    Var = "logo",
                    IsStatic = true,
                    StaticValue = "default.jpg",
                    Translations = new List<SettingTranslation>()
                    {
                        new SettingTranslation
                        {
                            Locale = "ar",
                            DisplayName = "الشعار",
                        },
                        new SettingTranslation
                        {
                            Locale = "en",
                            DisplayName = "Logo",
                        },
                    }
                };
                var site_desc = new Setting
                {
                    Type = 3,
                    Var = "site_desc",
                    IsStatic = false,
                    Translations = new List<SettingTranslation>()
                    {
                        new SettingTranslation
                        {
                            Locale = "ar",
                            Value = "وصف الموقع",
                            DisplayName = "وصف الموقع",
                        },
                        new SettingTranslation
                        {
                            Locale = "en",
                            Value = "description",
                            DisplayName = "Site Description",
                        },
                    }
                };

                context.Settings.Add(site_name);
                context.Settings.Add(logo);
                context.Settings.Add(site_desc);

                context.SaveChanges();
            }
        }
    }
}