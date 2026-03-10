using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http; // Bắt buộc để dùng Session
// bảo vệ cho tất cả trang thuộc admin dùng 1 lần
namespace QLBanNuoc.Controllers
{
    public class BaseAdminController : Controller
    {
        // Hàm này sẽ tự động chặn cửa kiểm tra TRƯỚC khi cho phép vào trang
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session.GetString("AdminLogin");

            if (string.IsNullOrEmpty(session))
            {
                // Nếu không có vé (chưa đăng nhập) Đá văng ra trang Login
                context.Result = new RedirectToActionResult("Login", "Admin", null);
            }

            base.OnActionExecuting(context);
        }
    }
}