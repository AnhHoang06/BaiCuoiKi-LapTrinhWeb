using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; 

namespace QLBanNuoc.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            var checkTicket = HttpContext.Session.GetString("AdminLogin");

            if (string.IsNullOrEmpty(checkTicket))
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            // Tài khoản cứng tạm thời (Sau này Dev 1 có DB thì thay bằng kiểm tra SQL)
            if (username == "admin" && password == "123")
            {
                // Cấp vé: Lưu một Session tên là "AdminLogin"
                HttpContext.Session.SetString("AdminLogin", "true");
                return RedirectToAction("Index"); // Chuyển hướng vào trong
            }

            ViewBag.Error = "Tài khoản hoặc mật khẩu không đúng!";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AdminLogin");
            return RedirectToAction("Login"); 
        }
    }
}