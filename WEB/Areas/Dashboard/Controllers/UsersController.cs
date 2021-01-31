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
    public class UsersController : BaseController
    {
        private readonly IStringLocalizer _localizer;

        public UsersController(DatabaseContext context, IStringLocalizer<UsersController> localizer) : base(context)
        {
            _localizer = localizer;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var rows = _context.Users.ToList();
            return View(rows);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult Create(User row)
        {
            if (ModelState.IsValid && !IsEmailExist(row.Email))
            {
                var password = BCrypt.Net.BCrypt.HashPassword(row.Password);
                row.Password = password;
                _context.Users.Add(row);
                _context.SaveChanges();
                TempData["msg"] = _localizer["added_successfully"].Value;
                return RedirectToAction("Index");
            }

            if (IsEmailExist(row.Email))
            {
                ModelState.AddModelError("Email",_localizer["Email_Exist"]);
            }
           
            return View();
        }
        
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var row = _context.Users.Find(id);
            return View(row);
        }
    
        [HttpPost("edit/{id}")]
        public IActionResult Edit(int id,User model)
        {
            ModelState.Remove("Password");
            if (ModelState.IsValid && !IsEmailExist(model.Email, a => a.Id != id))
            {
                var row = _context.Users.Find(id);
                if (!string.IsNullOrEmpty(model.Password))
                {
                    var password = BCrypt.Net.BCrypt.HashPassword(model.Password);
                    model.Password = password;
                }
                else
                {
                    model.Password = row.Password;
                }

                row.FirstName = model.FirstName;
                row.LastName = model.LastName;
                row.Email = model.Email;
                row.Type = model.Type;
                _context.SaveChanges();
                TempData["msg"] = _localizer["edited_successfully"].Value;
                return RedirectToAction("Index");
            }

            if (IsEmailExist(model.Email, a => a.Id != id))
            {
                ModelState.AddModelError("Email",_localizer["Email_Exist"]);
            }
           
            return View();
        }

        [HttpPost("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var row = _context.Users.FirstOrDefault(a => a.Id == id);
            _context.Users.Remove(row);
            _context.SaveChanges();
            TempData["msg"] = _localizer["deleted_successfully"].Value;
            return RedirectToAction("Index");
        }
        
        [HttpGet("is-email-exist")]
        public bool IsEmailExist(string email,Expression<Func<User,bool>> func = null)
        {
            var user = _context.Users.AsQueryable();
            if (func != null)
            {
                user = user.Where(func);
            }

            return user.FirstOrDefault(a=>a.Email == email) != null;
        }
        
    }
}