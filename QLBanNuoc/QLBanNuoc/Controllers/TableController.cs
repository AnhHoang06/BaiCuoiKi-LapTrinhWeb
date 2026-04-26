using Microsoft.AspNetCore.Mvc;
using QLBanNuoc.Models;

namespace QLBanNuoc.Controllers
{
    public class TableController : BaseAdminController
    {
        private readonly AppDbContext _context;

        public TableController(AppDbContext context)
        {
            _context = context;
        }

        // Trang danh sách bàn
        public IActionResult Index()
        {
            var tables = _context.CafeTables.OrderBy(t => t.TableNumber).ToList();
            return View(tables);
        }

        // Thêm bàn mới( TẠM THỜI KHÔNG SỬ DỤNG)
        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(CafeTable table)
        {
            if (ModelState.IsValid)
            {
                _context.CafeTables.Add(table);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(table);
        }

        // Xóa bàn
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var table = _context.CafeTables.Find(id);
            if (table != null)
            {
                _context.CafeTables.Remove(table);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // HÀM ĐẶC BIỆT: Đổi trạng thái nhanh
        [HttpPost]
        public IActionResult ChangeStatus(int id, string newStatus)
        {
            var table = _context.CafeTables.Find(id);
            if (table != null)
            {
                table.Status = newStatus;
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}