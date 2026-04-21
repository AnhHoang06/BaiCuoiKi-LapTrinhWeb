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
    }
}