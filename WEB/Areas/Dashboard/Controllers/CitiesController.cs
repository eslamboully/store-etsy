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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using WEB.Areas.Dashboard.ViewModels;
using WEB.Resources;

namespace WEB.Areas.Dashboard.Controllers
{
    [Authorize(AuthenticationSchemes = "admin")]
    public class CitiesController : BaseController
    {
        private readonly IStringLocalizer _localizer;

        public CitiesController(DatabaseContext context, IStringLocalizer<CitiesController> localizer) : base(context)
        {
            _localizer = localizer;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var rows = _context.Cities
                .Include("Translations").Include("Country").Include("Country.Translations").ToList();
            return View(rows);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            var countries = _context.Countries.Include("Translations").ToList();
            var row = new CityViewModel
            {
                Countries = countries
            };
            return View(row);
        }

        [HttpPost("create")]
        public IActionResult Create(CityViewModel model)
        {
            if (ModelState.IsValid)
            {
                var row = new City
                {
                    CountryId = model.CountryId,
                    Translations = model.Translations,
                };
                _context.Cities.Add(row);
                _context.SaveChanges();
                TempData["msg"] = _localizer["added_successfully"].Value;
                return RedirectToAction("Index");
            }
            var countries = _context.Countries.Include("Translations").ToList();
            model.Countries = countries;
            return View(model);
        }
        
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var row = _context.Cities.Include("Translations").Include("Country").SingleOrDefault(c=>c.Id == id);
            var countries = _context.Countries.Include("Translations").ToList();
            var model = new CityViewModel
            {
                CountryId = row.CountryId,
                Translations = row.Translations,
                Countries = countries
            };
            return View(model);
        }
    
        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id,CityViewModel model)
        {
            if (ModelState.IsValid)
            {
                var row = _context.Cities.Include("Translations").SingleOrDefault(c=>c.Id == id);
                row.CountryId = model.CountryId;
                row.Translations = model.Translations;
                _context.SaveChanges();
                TempData["msg"] = _localizer["edited_successfully"].Value;
                return RedirectToAction("Index");
            }
            var countries = _context.Countries.Include("Translations").ToList();
            model.Countries = countries;
            return View(model);
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var row = _context.Cities.FirstOrDefault(a => a.Id == id);
            _context.Cities.Remove(row);
            _context.SaveChanges();
            TempData["msg"] = _localizer["deleted_successfully"].Value;
            return RedirectToAction("Index");
        }
    }
}