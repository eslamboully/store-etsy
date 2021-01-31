using System;
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
    public class ColorsController : BaseController
    {
        private readonly IStringLocalizer _localizer;

        public ColorsController(DatabaseContext context, IStringLocalizer<ColorsController> localizer) : base(context)
        {
            _localizer = localizer;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var rows = _context.Colors.Include("Translations").ToList();
            return View(rows);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(ColorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var row = new Color
                {
                    Translations = model.Translations,
                    Degree = model.Degree
                };
                _context.Colors.Add(row);
                _context.SaveChanges();
                TempData["msg"] = _localizer["added_successfully"].Value;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var row = _context.Colors.Include("Translations").SingleOrDefault(c=>c.Id == id);
            var model = new ColorViewModel
            {
                Translations = row.Translations,
                Degree = row.Degree
            };
            return View(model);
        }
    
        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id,ColorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var row = _context.Colors.Include("Translations").SingleOrDefault(c=>c.Id == id);
                row.Degree = model.Degree;
                row.Translations = model.Translations;
                _context.SaveChanges();
                TempData["msg"] = _localizer["edited_successfully"].Value;
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var row = _context.Colors.FirstOrDefault(a => a.Id == id);
            _context.Colors.Remove(row);
            _context.SaveChanges();
            TempData["msg"] = _localizer["deleted_successfully"].Value;
            return RedirectToAction("Index");
        }
    }
}