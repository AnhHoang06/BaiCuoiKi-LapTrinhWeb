//using System.Diagnostics;
//using Microsoft.AspNetCore.Mvc;
//using QLBanNuoc.Models;
//// test git 
//namespace QLBanNuoc.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly ILogger<HomeController> _logger;

//        public HomeController(ILogger<HomeController> logger)
//        {
//            _logger = logger;
//        }

//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult Privacy()
//        {
//            return View();
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}


using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLBanNuoc.Models;
using QLBanNuoc.ViewModels;

namespace QLBanNuoc.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(AppDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: /  hoặc /Home/Index
        public async Task<IActionResult> Index()
        {
            var vm = new HomeIndexViewModel
            {
                Drinks = await _context.Drinks
                    .Include(d => d.Category)
                    .Where(d => d.IsAvailable)
                    .OrderBy(d => d.CategoryId)
                    .ToListAsync(),
                Categories = await _context.Categories.ToListAsync()
            };
            return View(vm);
        }

        // GET: /Home/Cart
        public IActionResult Cart()
        {
            return View();
        }

        // POST: /Home/PlaceOrder  (nhận JSON từ trang giỏ hàng)
        [HttpPost]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderRequest req)
        {
            if (req == null || req.Items == null || !req.Items.Any())
                return Json(new { success = false, message = "Giỏ hàng trống." });

            // Xử lý bàn (chỉ khi TaiQuan)
            int? tableId = null;
            if (req.OrderType == "TaiQuan" && req.TableNumber.HasValue)
            {
                var table = await _context.CafeTables
                    .FirstOrDefaultAsync(t => t.TableNumber == req.TableNumber.Value);
                if (table == null)
                    return Json(new { success = false, message = $"Không tìm thấy bàn số {req.TableNumber}." });
                tableId = table.Id;
            }

            var order = new Order
            {
                CustomerName = req.CustomerName,
                Phone = req.Phone,
                OrderType = req.OrderType,
                TableId = tableId,
                DeliveryAddress = req.DeliveryAddress,
                Note = req.Note,
                Status = "ChoXacNhan",
                TotalPrice = req.TotalPrice,
                CreatedAt = DateTime.Now
            };

            foreach (var item in req.Items)
            {
                var drink = await _context.Drinks.FindAsync(item.DrinkId);
                if (drink == null) continue;
                order.OrderItems.Add(new OrderItem
                {
                    DrinkId = item.DrinkId,
                    Quantity = item.Qty,
                    Price = drink.Price
                });
            }

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Json(new { success = true, orderId = order.Id });
        }

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() =>
            View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}