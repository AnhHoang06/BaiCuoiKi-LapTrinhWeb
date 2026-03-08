using Microsoft.AspNetCore.Mvc;

namespace QLBanNuoc.Controllers
{
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}