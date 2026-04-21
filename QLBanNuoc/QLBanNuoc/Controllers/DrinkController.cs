using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using QLBanNuoc.Models;

namespace QLBanNuoc.Controllers
{
    // Vẫn kế thừa 
    public class DrinkController : BaseAdminController
    {
        private readonly AppDbContext _context;

        // Yêu cầu hệ thống cấp quyền truy cập Database 
        public DrinkController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Lấy danh sách nước từ SQL, kèm theo tên Danh mục của nó
            var realDrinks = _context.Drinks.Include(d => d.Category).ToList();
            return View(realDrinks);
        }
    }
}