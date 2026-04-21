using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; // Cần thiết để làm Dropdown danh mục
using Microsoft.EntityFrameworkCore;
using QLBanNuoc.Models;

namespace QLBanNuoc.Controllers
{
    public class DrinkController : BaseAdminController
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env; // Công cụ chỉ đường đến thư mục wwwroot

        public DrinkController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult Index()
        {
            var realDrinks = _context.Drinks.Include(d => d.Category).ToList();
            return View(realDrinks);
        }

        // 1. Mở Form Thêm Mới
        [HttpGet]
        public IActionResult Create()
        {
            // Lấy danh sách Danh mục từ DB để thả vào ô Dropdown (Select)
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            return View();
        }

        // 2. Hứng dữ liệu khi Admin bấm nút Lưu
        [HttpPost]
        public IActionResult Create(Drink drink, IFormFile? imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                // Đường dẫn tới wwwroot/images/Item
                string uploadsFolder = Path.Combine(_env.WebRootPath, "images", "Item");

                // Tự động tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Tạo tên file ngẫu nhiên để không bị trùng (VD: asdf-1234.jpg)
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Copy file vào thư mục
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }

                // Lưu đường dẫn vào cột ImageUrl của SQL
                drink.ImageUrl = "/images/Item/" + uniqueFileName;
            }

            _context.Drinks.Add(drink);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        // 1. Mở trang Chỉnh sửa và đổ dữ liệu cũ vào Form
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var drink = _context.Drinks.Find(id);
            if (drink == null) return NotFound();

            // Chuẩn bị danh sách danh mục và chọn sẵn cái danh mục hiện tại của món
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name", drink.CategoryId);
            return View(drink);
        }

        // 2. nhấn "Lưu thay đổi"
        [HttpPost]
        public IActionResult Edit(int id, Drink drink, IFormFile? imageFile)
        {
            if (id != drink.Id) return NotFound();

            // Tìm món cũ trong DB để lấy lại đường dẫn ảnh cũ nếu Admin không đổi ảnh
            var oldDrink = _context.Drinks.AsNoTracking().FirstOrDefault(d => d.Id == id);
            if (oldDrink == null) return NotFound();

            if (imageFile != null && imageFile.Length > 0)
            {
                // Nếu có chọn ảnh mới -> Xử lý lưu file y hệt như hàm Create
                string uploadsFolder = Path.Combine(_env.WebRootPath, "images", "Item");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }
                drink.ImageUrl = "/images/Item/" + uniqueFileName;
            }
            else
            {
                // Nếu không chọn ảnh mới -> Giữ nguyên đường dẫn ảnh cũ từ DB
                drink.ImageUrl = oldDrink.ImageUrl;
            }

            _context.Update(drink);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        // Xử lý Xóa món nước
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var drink = _context.Drinks.Find(id);
            if (drink == null) return NotFound();

            // 1. LẤY ĐƯỜNG DẪN VẬT LÝ CỦA ẢNH
            if (!string.IsNullOrEmpty(drink.ImageUrl))
            {
                // Chuyển link "/images/Item/abc.jpg" thành đường dẫn ổ cứng "C:\Project\wwwroot\images\Item\abc.jpg"
                var imagePath = Path.Combine(_env.WebRootPath, drink.ImageUrl.TrimStart('/'));

                // 2. KIỂM TRA VÀ XÓA FILE ẢNH TRONG WWWROOT
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath);
                }
            }

            // 3. XÓA BẢN GHI TRONG DATABASE
            _context.Drinks.Remove(drink);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}