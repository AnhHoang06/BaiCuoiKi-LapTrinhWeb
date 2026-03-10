using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace QLBanNuoc.Controllers
{
    // Kế thừa BaseAdminController để kích hoạt bảo vệ
    public class DrinkController : BaseAdminController
    {
        public IActionResult Index()
        {
            // Dữ liệu giả (Mock data)
            var mockDrinks = new List<dynamic>
            {
                new { Id = 1, Name = "Trà sữa LAT trân châu", Category = "Trà sữa", Price = 35000, Status = "Còn hàng" },
                new { Id = 2, Name = "Cà phê muối", Category = "Cà phê", Price = 25000, Status = "Còn hàng" },
                new { Id = 3, Name = "Trà đào cam sả", Category = "Trà trái cây", Price = 30000, Status = "Hết hàng" }
            };

            return View(mockDrinks);
        }
    }
}