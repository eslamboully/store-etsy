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
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var row = new Category
                {
                    ParentId = model.ParentId,
                    Translations = model.Translations,
                };
                _context.Categories.Add(row);
                _context.SaveChanges();
                TempData["msg"] = _localizer["added_successfully"].Value;
                return RedirectToAction("Index");
            }
            return View(model);
        }
        
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var row = _context.Categories.Include("Translations").SingleOrDefault(c=>c.Id == id);
            var model = new CategoryViewModel
            {
                Translations = row.Translations,
                ParentId = row.ParentId
            };
            return View(model);
        }
    
        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id,CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var row = _context.Categories.Include("Translations").SingleOrDefault(c=>c.Id == id);
                row.ParentId = model.ParentId;
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