using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLBanNuoc.Models;
using System.Linq;

namespace QLBanNuoc.Controllers
{
    public class OrderController : BaseAdminController
    {
        private readonly AppDbContext _context;

        public OrderController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Manage(string status, int page = 1)
        {
            int pageSize = 5;
            var query = _context.Orders.Include(o => o.CafeTable).AsQueryable();

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(o => o.Status == status);
            }

            int totalItems = query.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var orders = query.OrderByDescending(o => o.CreatedAt)
                              .Skip((page - 1) * pageSize)
                              .Take(pageSize)
                              .ToList();

            ViewBag.CurrentStatus = status;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(orders);
        }

        // XEM CHI TIẾT ĐƠN HÀNG
        [HttpGet]
        public IActionResult Details(int id)
        {
            var order = _context.Orders
                .Include(o => o.CafeTable)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Drink)
                .FirstOrDefault(o => o.Id == id);

            if (order == null) return NotFound();

            // Lấy danh sách các bàn đang TRỐNG để hiện vào Dropdown gán bàn
            // Chỉ lấy những bàn có Status là "Trong"
            ViewBag.AvailableTables = _context.CafeTables
                                              .Where(t => t.Status == "Trong")
                                              .OrderBy(t => t.TableNumber)
                                              .ToList();

            return View(order);
        }

        // HÀM XỬ LÝ GÁN BÀN VÀ TỰ ĐỘNG ĐỔI TRẠNG THÁI
        [HttpPost]
        public IActionResult AssignTable(int orderId, int tableId)
        {
            var order = _context.Orders.Find(orderId);
            var table = _context.CafeTables.Find(tableId);

            if (order != null && table != null)
            {
                // Bước 1: Gán mã bàn cho đơn hàng
                // Xóa dòng bị lỗi: order.CafeTableId = tableId;
                // Tham chiếu đối tượng để EF tự động cập nhật khóa ngoại
                order.CafeTable = table;

                // Bước 2: Tự động chuyển trạng thái bàn sang "DangSuDung"
                // Đây là chỗ giúp sếp không phải làm thủ công 2 nơi
                table.Status = "DangSuDung";

                _context.SaveChanges();
            }

            // Quay lại trang chi tiết của chính đơn hàng đó
            return RedirectToAction("Details", new { id = orderId });
        }

        [HttpGet]
        public IActionResult UpdateStatus(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null) return NotFound();
            return View(order);
        }

        [HttpPost]
        public IActionResult UpdateStatus(int id, string status)
        {
            var order = _context.Orders.Find(id);
            if (order == null) return NotFound();

            order.Status = status;
            _context.SaveChanges();

            return RedirectToAction("Manage");
        }
    }
}