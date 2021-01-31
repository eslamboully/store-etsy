using System;
using System.Linq;
using System.Linq.Expressions;
using Core.Areas.Dashboard.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using WEB.Resources;

namespace WEB.Areas.Dashboard.Controllers
{
    [Authorize(AuthenticationSchemes = "admin")]
    public class AdminsController : BaseController
    {
        private readonly IStringLocalizer _localizer;

        public AdminsController(DatabaseContext context, IStringLocalizer<AdminsController> localizer) : base(context)
        {
            _localizer = localizer;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var rows = _context.Admins.Where(a=>a.Id != 1).ToList();
            return View(rows);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(Admin admin)
        {
            if (ModelState.IsValid && !IsEmailExist(admin.Email))
            {
                var password = BCrypt.Net.BCrypt.HashPassword(admin.Password);
                admin.Password = password;
                _context.Admins.Add(admin);
                _context.SaveChanges();
                TempData["msg"] = "Added Successfully";
                return RedirectToAction("Index");
            }

            if (IsEmailExist(admin.Email))
            {
                ModelState.AddModelError("Email",_localizer["Email_Exist"]);
            }
           
            return View();
        }
        
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var admin = _context.Admins.Find(id);
            return View(admin);
        }

        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id,Admin admin)
        {
            ModelState.Remove("Password");
            if (ModelState.IsValid && !IsEmailExist(admin.Email, a => a.Id != id))
            {
                var row = _context.Admins.Find(id);
                if (!string.IsNullOrEmpty(admin.Password))
                {
                    var password = BCrypt.Net.BCrypt.HashPassword(admin.Password);
                    admin.Password = password;
                }
                else
                {
                    admin.Password = row.Password;
                }

                row.FirstName = admin.FirstName;
                row.LastName = admin.LastName;
                row.Email = admin.Email;
                _context.SaveChanges();
                TempData["msg"] = "Edited Successfully";
                return RedirectToAction("Index");
            }

            if (IsEmailExist(admin.Email, a => a.Id != id))
            {
                ModelState.AddModelError("Email",_localizer["Email_Exist"]);
            }
           
            return View();
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var row = _context.Admins.FirstOrDefault(a => a.Id == id);
            _context.Admins.Remove(row);
            _context.SaveChanges();
            TempData["msg"] = "Deleted Successfully";
            return RedirectToAction("Index");
        }
        
        [HttpGet("is-email-exist")]
        public bool IsEmailExist(string email,Expression<Func<Admin,bool>> func = null)
        {
            var user = _context.Admins.AsQueryable();
            if (func != null)
            {
                user = user.Where(func);
            }

            return user.FirstOrDefault(a=>a.Email == email) != null;
        }
        
    }
}