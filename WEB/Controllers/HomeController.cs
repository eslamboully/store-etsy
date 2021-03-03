using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using WEB.Areas.Dashboard.Controllers;

namespace WEB.Controllers
{
    [Route("/")]
    public class HomeController : Controller
    {
        private readonly IStringLocalizer _localizer;
        private readonly DatabaseContext _context;
        public HomeController(DatabaseContext context, IStringLocalizer<AdminsController> localizer)
        {
            _localizer = localizer;
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}