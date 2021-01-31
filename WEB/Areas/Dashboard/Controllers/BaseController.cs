using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;

namespace WEB.Areas.Dashboard.Controllers
{
    [Area("Dashboard"),Route("dashboard/[controller]")]
    public class BaseController : Controller
    {
        protected readonly DatabaseContext _context;

        public BaseController(DatabaseContext context)
        {
            _context = context;
        }
    }
}