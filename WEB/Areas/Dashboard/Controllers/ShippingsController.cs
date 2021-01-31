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
    public class ShippingsController : BaseController
    {
        private readonly IStringLocalizer _localizer;

        public ShippingsController(DatabaseContext context, IStringLocalizer<ShippingsController> localizer) : base(context)
        {
            _localizer = localizer;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var rows = _context.Shippings.Include("Translations").Include("User").ToList();
            return View(rows);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            var users = _context.Users.Where(c=>c.Type == 3).ToList();
            var model = new ShippingViewModel
            {
                Users = users
            };
            return View(model);
        }

        [HttpPost("create")]
        public IActionResult Create(ShippingViewModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = "default.jpg";
                if (model.Logo != null)
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Logo.FileName);
                    string savePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/dashboard/uploads/shippings",fileName);
                    using(var stream=new FileStream(savePath, FileMode.Create))
                    {
                        model.Logo.CopyTo(stream);
                    }
                }
                var row = new Shipping
                {
                    Logo = fileName,
                    Translations = model.Translations,
                    Address = model.Address,
                    Lat = model.Lat,
                    Lng = model.Lng,
                    UserId = model.UserId
                };
                _context.Shippings.Add(row);
                _context.SaveChanges();
                TempData["msg"] = _localizer["added_successfully"].Value;
                return RedirectToAction("Index");
            }
            var users = _context.Users.Where(c=>c.Type == 3).ToList();
            model.Users = users;
            return View(model);
        }
        
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var row = _context.Shippings.Include("Translations").SingleOrDefault(c=>c.Id == id);
            var users = _context.Users.Where(c=>c.Type == 3).ToList();
            var model = new ShippingViewModel
            {
                Translations = row.Translations,
                Lat = row.Lat,
                Lng = row.Lng,
                Address = row.Address,
                Users = users,
                UserId = row.UserId
            };
            return View(model);
        }
    
        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id,ShippingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var row = _context.Shippings.Include("Translations").SingleOrDefault(c=>c.Id == id);
                // Update Logo
                string fileName = row.Logo;
                if (model.Logo != null)
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Logo.FileName);
                    string savePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/dashboard/uploads/shippings",fileName);
                    using(var stream=new FileStream(savePath, FileMode.Create))
                    {
                        model.Logo.CopyTo(stream);
                        if (row.Logo != "default.jpg")
                        {
                            System.IO.File.Delete(Path.Combine("wwwroot/dashboard/uploads/shippings",row.Logo)); 
                        }
                    }
                }
                row.Lat = model.Lat;
                row.Lng = model.Lng;
                row.Logo = fileName;
                row.Translations = model.Translations;
                row.Address = model.Address;
                row.UserId = model.UserId;

                _context.SaveChanges();
                TempData["msg"] = _localizer["edited_successfully"].Value;
                return RedirectToAction("Index");
            }
            var users = _context.Users.Where(c=>c.Type == 3).ToList();
            model.Users = users;
            return View(model);
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var row = _context.Shippings.FirstOrDefault(a => a.Id == id);
            _context.Shippings.Remove(row);
            _context.SaveChanges();
            TempData["msg"] = _localizer["deleted_successfully"].Value;
            return RedirectToAction("Index");
        }
    }
}