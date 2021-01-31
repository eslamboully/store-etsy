using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;

namespace WEB.Areas.Dashboard.Controllers
{
    [Route("dashboard")]
    public class DashboardController : BaseController
    {
        public DashboardController(DatabaseContext context) : base(context) {}
        
        [HttpGet,Authorize(AuthenticationSchemes = "admin")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost("login")]
        public IActionResult LoginPost(string email, string password)
        {
            var admin = _context.Admins.SingleOrDefault(a => a.Email.Equals(email));
            if (admin != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password,admin.Password))
                {
                    var adminClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, admin.FirstName + ' '+ admin.LastName),
                        new Claim(ClaimTypes.Email, admin.Email),
                        new Claim(ClaimTypes.Role, "admin"),
                        new Claim("Email", admin.Email)
                    };
                    var adminClaimsIdentity = new ClaimsIdentity(adminClaims,"admin");
                    var adminClaimsPrincipal = new ClaimsPrincipal(adminClaimsIdentity);
                    HttpContext.SignInAsync("admin",adminClaimsPrincipal);
                    return RedirectToAction("Index");
                }
            }

            TempData["error"] = "خطأ في البريد الالكتروني او كلمة المرور";
            return RedirectToAction("Login");
        }
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync("admin");
            return RedirectToAction("Login");
        }
        
        [HttpGet("change-language/{locale}")]
        public IActionResult ChangeLanguage(string locale)
        {
            Response.Cookies.Append(
                "AdminLocale",
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(locale)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}