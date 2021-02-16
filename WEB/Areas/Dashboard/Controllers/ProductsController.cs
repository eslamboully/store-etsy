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
            var rows = _context.Products
                .Include(p => p.Translations)
                .Include(p=>p.Category)
                .Include(p=>p.Category.Translations)
                .ToList();
            return View(rows);
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
        public IActionResult Create(ProductViewModel model,string save,string saveAndContinue)
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
                    Status = 0,
                    CategoryId = model.CategoryId,
                    SizeString = model.SizeString,
                    Sizes = _context.Sizes.Where( s => model.SizesArray.Contains(s.Id)).ToList(),
                    Countries = _context.Countries.Where( s => model.CountriesArray.Contains(s.Id)).ToList(),
                    Colors = _context.Colors.Where( s => model.ColorsArray.Contains(s.Id)).ToList(),
                    Translations = model.Translations,
                    Photo = fileName
                };
                
                _context.Products.Add(row);
                _context.SaveChanges();
                TempData["msg"] = _localizer["added_successfully"].Value;
                
                if (!string.IsNullOrEmpty(save))
                {
                    return RedirectToAction("Create","ProductsPhoto",new {id = row.Id});
                } else if (!string.IsNullOrEmpty(saveAndContinue))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index");
                }
                
            }
            model.Sizes = _context.Sizes.Include("Translations").ToList();
            model.Countries = _context.Countries.Include("Translations").ToList();
            model.Colors = _context.Colors.Include("Translations").ToList();
            return View(model);
        }
        
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var row = _context.Products.Include("Translations")
                .Include(s=>s.Sizes)
                .Include(s=>s.Countries)
                .Include(s=>s.Colors)
                .SingleOrDefault(c=>c.Id == id);
            var model = new ProductViewModel
            {
                Sizes = _context.Sizes.Include("Translations").ToList(),
                Countries = _context.Countries.Include("Translations").ToList(),
                Colors = _context.Colors.Include("Translations").ToList(),
                Price = row.Price,
                PriceOffer = row.PriceOffer,
                StartOfferAt = row.StartOfferAt,
                EndOfferAt = row.EndOfferAt,
                CategoryId = row.CategoryId,
                SizeString = row.SizeString,
                Translations = row.Translations,
                Reason = row.Reason,
                Status = row.Status,
                SizesArray = row.Sizes.Select(s=>s.Id).ToList(),
                CountriesArray = row.Countries.Select(s=>s.Id).ToList(),
                ColorsArray = row.Colors.Select(s=>s.Id).ToList()
            };
            return View(model);
        }
    
        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id,ProductViewModel model)
        {
            var row = _context.Products.Include("Translations")
                .Include(s=>s.Sizes)
                .Include(s=>s.Countries)
                .Include(s=>s.Colors)
                .SingleOrDefault(c=>c.Id == id);
            
            if (ModelState.IsValid)
            {
                row.Price = model.Price;
                row.PriceOffer = model.PriceOffer;
                row.StartOfferAt = model.StartOfferAt;
                row.EndOfferAt = model.EndOfferAt;
                row.CategoryId = model.CategoryId;
                row.SizeString = model.SizeString;
                row.Status = model.Status;
                row.Reason = model.Reason;
                row.Sizes = _context.Sizes.Where(s => model.SizesArray.Contains(s.Id)).ToList();
                row.Countries = _context.Countries.Where(s => model.CountriesArray.Contains(s.Id)).ToList();
                row.Colors = _context.Colors.Where(s => model.ColorsArray.Contains(s.Id)).ToList();
                row.Translations = model.Translations;
                
                _context.SaveChanges();
                TempData["msg"] = _localizer["edited_successfully"].Value;
                
                return RedirectToAction("Index");
            
            }
            
            model.Sizes = _context.Sizes.Include("Translations").ToList();
            model.Countries = _context.Countries.Include("Translations").ToList();
            model.Colors = _context.Colors.Include("Translations").ToList();
            model.Price = row.Price;
            model.PriceOffer = row.PriceOffer;
            model.StartOfferAt = row.StartOfferAt;
            model.EndOfferAt = row.EndOfferAt;
            model.CategoryId = row.CategoryId;
            model.SizeString = row.SizeString;
            model.Translations = row.Translations;
            model.SizesArray = _context.Sizes.Select(s => s.Id).ToList();
            model.CountriesArray = _context.Countries.Select(s => s.Id).ToList();
            model.ColorsArray = _context.Colors.Select(s => s.Id).ToList();
            
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