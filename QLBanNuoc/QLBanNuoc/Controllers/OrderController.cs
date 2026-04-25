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

            // 1. Bắt đầu với toàn bộ đơn hàng
            var query = _context.Orders.Include(o => o.CafeTable).AsQueryable();

            // 2. LOGIC LỌC: Nếu chọn một trạng thái cụ thể
            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(o => o.Status == status);
            }

            // 3. LOGIC PHÂN TRANG: Tính toán số trang
            int totalItems = query.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            // Bỏ qua các đơn của trang trước và lấy số lượng đơn của trang hiện tại
            var orders = query.OrderByDescending(o => o.CreatedAt)
                              .Skip((page - 1) * pageSize)
                              .Take(pageSize)
                              .ToList();

            // Gửi các thông số này ra View để vẽ nút bấm
            ViewBag.CurrentStatus = status;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(orders);
        }
        // XEM CHI TIẾT ĐƠN HÀNG
        [HttpGet]
        public IActionResult Details(int id)
        {
            // Tìm đơn hàng có ID tương ứng
            var order = _context.Orders
                .Include(o => o.CafeTable) // Lấy thông tin Bàn (nếu có)
                .Include(o => o.OrderItems) // Lấy danh sách các dòng chi tiết (chứa DrinkId, Số lượng)
                .ThenInclude(oi => oi.Drink) // TỪ dòng chi tiết, móc nối sang bảng Drink để lấy Tên món, Hình ảnh
                .FirstOrDefault(o => o.Id == id);

            if (order == null) return NotFound();

            return View(order);
        }
        // 1. Mở trang chọn Trạng thái mới
        [HttpGet]
        public IActionResult UpdateStatus(int id)
        {
            var order = _context.Orders.Find(id);
            if (order == null) return NotFound();
            return View(order);
        }

        // 2. Xử lý lưu Trạng thái mới vào Database
        [HttpPost]
        public IActionResult UpdateStatus(int id, string status)
        {
            var order = _context.Orders.Find(id);
            if (order == null) return NotFound();

            // Cập nhật trạng thái
            order.Status = status;
            _context.SaveChanges();

            return RedirectToAction("Manage");
        }
    }
}