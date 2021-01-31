using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Core.Areas.Dashboard.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using WEB.Areas.Dashboard.ViewModels;
using WEB.Resources;

namespace WEB.Areas.Dashboard.Controllers
{
    [Authorize(AuthenticationSchemes = "admin")]
    public class CountriesController : BaseController
    {
        private readonly IStringLocalizer _localizer;

        public CountriesController(DatabaseContext context, IStringLocalizer<CountriesController> localizer) : base(context)
        {
            _localizer = localizer;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var rows = _context.Countries.Include("Translations").ToList();
            return View(rows);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(CountryViewModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = "default.jpg";
                if (model.Logo != null)
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Logo.FileName);
                    string savePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/dashboard/uploads/countries",fileName);
                    using(var stream=new FileStream(savePath, FileMode.Create))
                    {
                        model.Logo.CopyTo(stream);
                    }
                }
                var row = new Country
                {
                    Mob = model.Mob,
                    Code = model.Code,
                    Translations = model.Translations,
                    Logo = fileName
                };
                _context.Countries.Add(row);
                _context.SaveChanges();
                TempData["msg"] = _localizer["added_successfully"].Value;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var row = _context.Countries.Include("Translations").SingleOrDefault(c=>c.Id == id);
            var model = new CountryViewModel
            {
                Mob = row.Mob,
                Code = row.Code,
                Translations = row.Translations
            };
            return View(model);
        }
    
        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id,CountryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var row = _context.Countries.Include("Translations").SingleOrDefault(c=>c.Id == id);
                // Update Logo
                string fileName = row.Logo;
                if (model.Logo != null)
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Logo.FileName);
                    string savePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/dashboard/uploads/countries",fileName);
                    using(var stream=new FileStream(savePath, FileMode.Create))
                    {
                        model.Logo.CopyTo(stream);
                        if (row.Logo != "default.jpg")
                        {
                            System.IO.File.Delete(Path.Combine("wwwroot/dashboard/uploads/countries",row.Logo)); 
                        }
                    }
                }
                // Update Database
                row.Translations = model.Translations;
                row.Mob = model.Mob;
                row.Code = model.Code;
                row.Logo = fileName;
                _context.SaveChanges();
                TempData["msg"] = _localizer["edited_successfully"].Value;
                return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var row = _context.Countries.FirstOrDefault(a => a.Id == id);
            _context.Countries.Remove(row);
            _context.SaveChanges();
            TempData["msg"] = _localizer["deleted_successfully"].Value;
            return RedirectToAction("Index");
        }
    }
}