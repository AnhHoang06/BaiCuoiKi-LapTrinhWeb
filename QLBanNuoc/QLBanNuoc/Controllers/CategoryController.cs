using Microsoft.AspNetCore.Mvc;
using QLBanNuoc.Models;
using System.Linq;

namespace QLBanNuoc.Controllers
{

    public class CategoryController : BaseAdminController
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }
        // ================= THÊM MỚI =================
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // ================= CHỈNH SỬA =================
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        public IActionResult Edit(int id, Category category)
        {
            if (id != category.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // ================= XÓA =================
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null) return NotFound();

            try
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Đã xóa danh mục thành công!";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Không thể xóa! Vui lòng xóa hết đồ uống thuộc danh mục này trước.";
            }

            return RedirectToAction("Index");
        }
    }
}