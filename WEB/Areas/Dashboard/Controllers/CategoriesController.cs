using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Core.Areas.Dashboard.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Localization;
using WEB.Areas.Dashboard.ViewModels;

namespace WEB.Areas.Dashboard.Controllers
{
    [Authorize(AuthenticationSchemes = "admin")]
    public class CategoriesController : BaseController
    {
        private readonly IStringLocalizer _localizer;

        public CategoriesController(DatabaseContext context, IStringLocalizer<CategoriesController> localizer) : base(context)
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
            return View(new CategoryViewModel
            {
                Colors = _context.Colors.Include(c=>c.Translations).ToList()
            });
        }

        [HttpPost("create")]
        public IActionResult Create(CategoryViewModel model,int[] colors,IFormFile[] colorsPhotos)
        {
            if (ModelState.IsValid)
            {
                string fileName = "default.jpg";
                if (model.Photo != null)
                {
                    fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Photo.FileName);
                    string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/dashboard/uploads/categories", fileName);
                    using (var stream = new FileStream(savePath, FileMode.Create))
                    {
                        model.Photo.CopyTo(stream);
                    }
                }
                var row = new Category
                {
                    ParentId = model.ParentId,
                    Translations = model.Translations,
                    Photo = fileName
                };
                // Add Categories
                _context.Categories.Add(row);
                _context.SaveChanges();
                
                // Add Category-Color Relation
                if ((colors.Length != 0 && colorsPhotos.Length != 0) && colors.Length == colorsPhotos.Length)
                {
                    for(int i=0;i<colors.Length;i++)
                    {
                        // save colorPhoto
                        fileName = Guid.NewGuid().ToString() + Path.GetExtension(colorsPhotos[i].FileName);
                        string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/dashboard/uploads/categories-colors", fileName);
                        using (var stream = new FileStream(savePath, FileMode.Create)) { colorsPhotos[i].CopyTo(stream); }
                    
                        // save Relation
                        _context.CategoryColors.Add(new CategoryColor
                        {
                            CategoryId = row.Id,
                            ColorId = colors[i],
                            Photo = fileName
                        });
                    }

                    _context.SaveChanges();
                }
                
                
                TempData["msg"] = _localizer["added_successfully"].Value;
                return RedirectToAction("Index");
            }

            model.Colors = _context.Colors.Include(c => c.Translations).ToList();
            return View(model);
        }
        
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var row = _context.Categories.Include("Translations").SingleOrDefault(c=>c.Id == id);
            var model = new CategoryViewModel
            {
                Translations = row.Translations,
                ParentId = row.ParentId,
                PhotoName = row.Photo,
                Photo = null
            };
            return View(model);
        }
    
        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id,CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var row = _context.Categories.Include("Translations").SingleOrDefault(c=>c.Id == id);
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
                row.ParentId = model.ParentId;
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
            var row = _context.Categories.FirstOrDefault(a => a.Id == id);
            var rows = _context.Categories.Where(c=>c.ParentId == row.Id);
            _context.Categories.RemoveRange(rows);
            _context.Categories.Remove(row);
            _context.SaveChanges();
            TempData["msg"] = _localizer["deleted_successfully"].Value;
            return RedirectToAction("Index");
        }

        [HttpPost("get-categories")]
        public IActionResult GetCategories(int? parentId,int? categoryId)
        {
            string culture = Thread.CurrentThread.CurrentCulture.Name;
            var rows = _context.Categories
                .Include("Translations")
                .Where(c=>c.Id != categoryId)
                .Select(c=> new
                {
                    id = c.Id.ToString(), 
                    parent = c.ParentId.ToString() != null ? c.ParentId.ToString() : "#" , 
                    text = c.Translatable(culture).Title,
                    state = new {
                        opened    = parentId.ToString() == c.Id.ToString() ? true : false,  // is the node open
                        disabled  = false, // is the node disabled
                        selected  = parentId.ToString() == c.Id.ToString() ? true : false,  // is the node selected
                    },
                })
                .ToList();
            return Json(rows);
        }
    }
}