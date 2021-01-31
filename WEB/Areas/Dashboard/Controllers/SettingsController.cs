using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Areas.Dashboard.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using WEB.Areas.Dashboard.ViewModels;

namespace WEB.Areas.Dashboard.Controllers
{
    [Authorize(AuthenticationSchemes = "admin")]
    public class SettingsController : BaseController
    {
        private readonly IStringLocalizer _localizer;

        public SettingsController(DatabaseContext context, IStringLocalizer<SettingsController> localizer) : base(context)
        {
            _localizer = localizer;
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var rows = _context.Settings.Include("Translations").ToList();
            var viewmodel = new SettingViewModel
            {
                Settings = rows
            };
            
            return View(viewmodel);
        }
    
        [HttpPost]
        public IActionResult Edit(SettingViewModel settingViewModel)
        {
            var rowSettings = _context.Settings.Include("Translations").ToList();
            // Start Edit Logic
            foreach (var rowSetting in rowSettings)
            {
                foreach (var requestSetting in settingViewModel.Settings)
                {
                    if (rowSetting.Var != requestSetting.Var)
                    {
                        if (rowSetting.IsStatic == false)
                        {
                            if (rowSetting.Type != 2)
                            {
                                rowSetting.Translations = requestSetting.Translations;
                            }
                        };
                    }
                }
            }

            // End Edit Logic
            _context.SaveChanges();
            TempData["msg"] = _localizer["edited_successfully"].Value;
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}