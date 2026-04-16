using Microsoft.AspNetCore.Mvc;
using QLBanNuoc.Models;

namespace QLBanNuoc.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            var admin = _context.Admins.FirstOrDefault(a => a.Username == username);

            if (admin == null || !BCrypt.Net.BCrypt.Verify(password, admin.PasswordHash))
            {
                ViewBag.Error = "Sai tài khoản hoặc mật khẩu";
                return View();
            }

            HttpContext.Session.SetString("Admin", admin.Username);

            return RedirectToAction("Index", "Admin");
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("Admin") == null)
                return RedirectToAction("Login");

            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}