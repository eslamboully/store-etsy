using System;
using System.Collections.Generic;
using System.IO;
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
    public class ProductsController : BaseController
    {
        private readonly IStringLocalizer _localizer;

        public ProductsController(DatabaseContext context, IStringLocalizer<ProductsController> localizer) : base(context)
        {
            _localizer = localizer;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            var model = new ProductViewModel
            {
                Sizes = _context.Sizes.Include("Translations").ToList(),
                Countries = _context.Countries.Include("Translations").ToList(),
                Colors = _context.Colors.Include("Translations").ToList()
            };
            return View(model);
        }

        [HttpPost("create")]
        public IActionResult Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = "default.jpg";
                var row = new Product
                {
                    Price = model.Price,
                    PriceOffer = model.PriceOffer,
                    StartOfferAt = model.StartOfferAt,
                    EndOfferAt = model.EndOfferAt,
                    Status = 1,
                    CategoryId = model.CategoryId,
                    SizeString = model.SizeString,
                    Sizes = _context.Sizes.Where( s => model.SizesArray.Contains(s.Id)).ToList(),
                    Countries = _context.Countries.Where( s => model.CountriesArray.Contains(s.Id)).ToList(),
                    Colors = _context.Colors.Where( s => model.ColorsArray.Contains(s.Id)).ToList()
                };
                
                _context.Products.Add(row);
                _context.SaveChanges();
                TempData["msg"] = _localizer["added_successfully"].Value;
                return RedirectToAction("Index");
            }
            model.Sizes = _context.Sizes.Include("Translations").ToList();
            model.Countries = _context.Countries.Include("Translations").ToList();
            model.Colors = _context.Colors.Include("Translations").ToList();
            return View(model);
        }
        
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var row = _context.Products.Include("Translations").SingleOrDefault(c=>c.Id == id);
            var model = new ProductViewModel
            {
                Translations = row.Translations,
                PhotoName = row.Photo,
                Photo = null
            };
            return View(model);
        }
    
        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id,ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var row = _context.Products.Include("Translations").SingleOrDefault(c=>c.Id == id);
                // Update Logo
                string fileName = row.Photo;
                if (model.Photo != null)
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Photo.FileName);
                    string savePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/dashboard/uploads/categories",fileName);
                    using(var stream=new FileStream(savePath, FileMode.Create))
                    {
                        model.Photo.CopyTo(stream);
                        if (row.Photo != "default.jpg")
                        {
                            System.IO.File.Delete(Path.Combine("wwwroot/dashboard/uploads/countries",row.Photo)); 
                        }
                    }
                }
                
                row.Translations = model.Translations;
                row.Photo = fileName;
                _context.SaveChanges();
                TempData["msg"] = _localizer["edited_successfully"].Value;
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var row = _context.Products.FirstOrDefault(a => a.Id == id);
            _context.Products.Remove(row);
            _context.SaveChanges();
            TempData["msg"] = _localizer["deleted_successfully"].Value;
            return RedirectToAction("Index");
        }
    }
}