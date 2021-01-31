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
    public class MallsController : BaseController
    {   
        private readonly IStringLocalizer _localizer;

        public MallsController(DatabaseContext context, IStringLocalizer<MallsController> localizer) : base(context)
        {
            _localizer = localizer;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var rows = _context.Malls.Include("Translations").ToList();
            return View(rows);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(MallViewModel model)
        {
            if (ModelState.IsValid)
            {
                var row = new Mall
                {
                    Translations = model.Translations,
                    Phone = model.Phone,
                    Address = model.Address,
                    Email = model.Email,
                    Owner = model.Owner
                };
                _context.Malls.Add(row);
                _context.SaveChanges();
                TempData["msg"] = _localizer["added_successfully"].Value;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var row = _context.Malls.Include("Translations").SingleOrDefault(c=>c.Id == id);
            var model = new MallViewModel
            {
                Translations = row.Translations,
                Phone = row.Phone,
                Address = row.Address,
                Email = row.Email,
                Owner = row.Owner
            };
            return View(model);
        }
    
        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id,MallViewModel model)
        {
            if (ModelState.IsValid)
            {
                var row = _context.Malls.Include("Translations").SingleOrDefault(c=>c.Id == id);
                // Update Logo
                row.Translations = model.Translations;
                row.Phone = model.Phone;
                row.Address = model.Address;
                row.Email = model.Email;
                row.Owner = model.Owner;
                
                _context.SaveChanges();
                TempData["msg"] = _localizer["edited_successfully"].Value;
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var row = _context.Malls.FirstOrDefault(a => a.Id == id);
            _context.Malls.Remove(row);
            _context.SaveChanges();
            TempData["msg"] = _localizer["deleted_successfully"].Value;
            return RedirectToAction("Index");
        }
    }
}