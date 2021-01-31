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
    public class ManuFactsController : BaseController
    {
        private readonly IStringLocalizer _localizer;

        public ManuFactsController(DatabaseContext context, IStringLocalizer<ManuFactsController> localizer) : base(context)
        {
            _localizer = localizer;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var rows = _context.ManuFacts.Include("Translations").ToList();
            return View(rows);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(ManuFactViewModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = "default.jpg";
                if (model.Logo != null)
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Logo.FileName);
                    string savePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/dashboard/uploads/manufacts",fileName);
                    using(var stream=new FileStream(savePath, FileMode.Create))
                    {
                        model.Logo.CopyTo(stream);
                    }
                }
                var row = new ManuFact
                {
                    Logo = fileName,
                    Translations = model.Translations,
                    Facebook = model.Facebook,
                    Twitter = model.Twitter,
                    Lat = model.Lat,
                    Lng = model.Lng,
                    Phone = model.Phone,
                    ContactName = model.ContactName
                };
                _context.ManuFacts.Add(row);
                _context.SaveChanges();
                TempData["msg"] = _localizer["added_successfully"].Value;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var row = _context.ManuFacts.Include("Translations").SingleOrDefault(c=>c.Id == id);
            var model = new ManuFactViewModel
            {
                Translations = row.Translations,
                Facebook = row.Facebook,
                Lat = row.Lat,
                Lng = row.Lng,
                Phone = row.Phone,
                Twitter = row.Twitter,
                ContactName = row.ContactName
            };
            return View(model);
        }
    
        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id,ManuFactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var row = _context.ManuFacts.Include("Translations").SingleOrDefault(c=>c.Id == id);
                // Update Logo
                string fileName = row.Logo;
                if (model.Logo != null)
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Logo.FileName);
                    string savePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/dashboard/uploads/manufacts",fileName);
                    using(var stream=new FileStream(savePath, FileMode.Create))
                    {
                        model.Logo.CopyTo(stream);
                        if (row.Logo != "default.jpg")
                        {
                            System.IO.File.Delete(Path.Combine("wwwroot/dashboard/uploads/manufacts",row.Logo)); 
                        }
                    }
                }
                row.Lat = model.Lat;
                row.Lng = model.Lng;
                row.Logo = fileName;
                row.Phone = model.Phone;
                row.Translations = model.Translations;
                row.Twitter = model.Twitter;
                row.Facebook = model.Facebook;
                row.ContactName = model.ContactName;
                
                _context.SaveChanges();
                TempData["msg"] = _localizer["edited_successfully"].Value;
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var row = _context.ManuFacts.FirstOrDefault(a => a.Id == id);
            _context.ManuFacts.Remove(row);
            _context.SaveChanges();
            TempData["msg"] = _localizer["deleted_successfully"].Value;
            return RedirectToAction("Index");
        }
    }
}