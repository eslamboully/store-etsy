using System.Linq;
using System.Threading;
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
    public class StatesController : BaseController
    {
        private readonly IStringLocalizer _localizer;

        public StatesController(DatabaseContext context, IStringLocalizer<StatesController> localizer) : base(context)
        {
            _localizer = localizer;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var rows = _context.States
                .Include("Translations")
                .Include("Country").Include("Country.Translations")
                .Include("City").Include("City.Translations").ToList();
            return View(rows);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            var countries = _context.Countries.Include("Translations").ToList();
            var row = new StateViewModel
            {
                Countries = countries,
            };
            return View(row);
        }

        [HttpPost("create")]
        public IActionResult Create(StateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var row = new State
                {
                    CountryId = model.CountryId,
                    CityId = model.CityId,
                    Translations = model.Translations,
                };
                _context.States.Add(row);
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
            var row = _context.States.Include("Translations").Include("Country").Include("City").SingleOrDefault(c=>c.Id == id);
            var countries = _context.Countries.Include("Translations").ToList();
            var cities = _context.Cities.Include("Translations").Where(c=>c.CountryId == row.CountryId).ToList();
            var model = new StateViewModel
            {
                CountryId = row.CountryId,
                Translations = row.Translations,
                Countries = countries,
                Cities = cities
            };
            return View(model);
        }
    
        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id,StateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var row = _context.States.Include("Translations").SingleOrDefault(c=>c.Id == id);
                row.CountryId = model.CountryId;
                row.CityId = model.CityId;
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
            var row = _context.States.FirstOrDefault(a => a.Id == id);
            _context.States.Remove(row);
            _context.SaveChanges();
            TempData["msg"] = _localizer["deleted_successfully"].Value;
            return RedirectToAction("Index");
        }

        [HttpPost("specific-cities/{countryId}")]
        public IActionResult SpecificCities(int countryId)
        {
            string culture = Thread.CurrentThread.CurrentCulture.Name;
            var cities = _context.Cities
                .Include("Translations")
                .Where(c=>c.CountryId == countryId)
                .Select(c=>new {Id=c.Id,Title=c.Translatable(culture).Title })
                .ToList();
            return Json(cities);
        }
    }
}