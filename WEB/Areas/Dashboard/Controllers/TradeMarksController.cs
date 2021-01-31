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
    public class TradeMarksController : BaseController
    {
        private readonly IStringLocalizer _localizer;

        public TradeMarksController(DatabaseContext context, IStringLocalizer<TradeMarksController> localizer) : base(context)
        {
            _localizer = localizer;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var rows = _context.TradeMarks.Include("Translations").ToList();
            return View(rows);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(TradeMarkViewModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = "default.jpg";
                if (model.Logo != null)
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Logo.FileName);
                    string savePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/dashboard/uploads/trademarks",fileName);
                    using(var stream=new FileStream(savePath, FileMode.Create))
                    {
                        model.Logo.CopyTo(stream);
                    }
                }
                var row = new TradeMark
                {
                    Logo = fileName,
                    Translations = model.Translations
                };
                _context.TradeMarks.Add(row);
                _context.SaveChanges();
                TempData["msg"] = _localizer["added_successfully"].Value;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var row = _context.TradeMarks.Include("Translations").SingleOrDefault(c=>c.Id == id);
            var model = new TradeMarkViewModel
            {
                Translations = row.Translations,
            };
            return View(model);
        }
    
        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id,TradeMarkViewModel model)
        {
            if (ModelState.IsValid)
            {
                var row = _context.TradeMarks.Include("Translations").SingleOrDefault(c=>c.Id == id);
                // Update Logo
                string fileName = row.Logo;
                if (model.Logo != null)
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Logo.FileName);
                    string savePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/dashboard/uploads/trademarks",fileName);
                    using(var stream=new FileStream(savePath, FileMode.Create))
                    {
                        model.Logo.CopyTo(stream);
                        if (row.Logo != "default.jpg")
                        {
                            System.IO.File.Delete(Path.Combine("wwwroot/dashboard/uploads/trademarks",row.Logo)); 
                        }
                    }
                }
                row.Logo = fileName;
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
            var row = _context.TradeMarks.FirstOrDefault(a => a.Id == id);
            _context.TradeMarks.Remove(row);
            _context.SaveChanges();
            TempData["msg"] = _localizer["deleted_successfully"].Value;
            return RedirectToAction("Index");
        }
    }
}