using System;
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
    public class ProductsPhotoController : BaseController
    {
        private readonly IStringLocalizer _localizer;

        public ProductsPhotoController(DatabaseContext context, IStringLocalizer<ProductsPhotoController> localizer) : base(context)
        {
            _localizer = localizer;
        }

        [HttpGet("{id}/create")]
        public IActionResult Create(int id)
        {
            var product = _context.Products.Include(p => p.Category).SingleOrDefault(p => p.Id == id);
            var model = new ProductPhotoViewModel
            {
                Id = id,
                CategoryPhoto = product.Category.Photo,
                OldPhoto = product.Photo
            };
            
            return View(model);
        }

        [HttpPost("{id}/create")]
        public IActionResult Create(ProductPhotoViewModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Photo.FileName);
                string savePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/dashboard/uploads/products",fileName);
                using (var stream = new FileStream(savePath, FileMode.Create))
                {
                    model.Photo.CopyTo(stream);
                }

                var product = _context.Products.SingleOrDefault(p => p.Id == model.Id);
                product.Photo = fileName;
                product.Status = 1;
                _context.SaveChanges();
                TempData["msg"] = _localizer["added_successfully"].Value;
                return RedirectToAction("index","Products");
            };
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
                return RedirectToAction("Create");
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
            return RedirectToAction("Create");
        }

        [HttpPost("get-product-photos")]
        public IActionResult GetProductPhotos(int productId)
        {
            string culture = Thread.CurrentThread.CurrentCulture.Name;
            var product = _context.Products.First(c => c.Id == productId);
            var categoryColors = _context.CategoryColors
                .Include("Color")
                .Where(c => c.CategoryId == product.CategoryId)
                .Select(c=> new
                {
                    name = c.Color.Degree,
                    color = c.Color.Degree,
                    image = new
                    {
                        url = "https://localhost:5001/dashboard/uploads/categories-colors/"+c.Photo,
                        name = "dsadsa",
                    },
                    active = true
                })
                .ToArray();
            return Json(new
            {
                colors = categoryColors
            });
    }
    }
}