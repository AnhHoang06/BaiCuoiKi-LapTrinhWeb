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

        public IActionResult Manage()
        {

            var orders = _context.Orders
                                 .Include(o => o.CafeTable)
                                 .OrderByDescending(o => o.CreatedAt)
                                 .ToList();
            return View(orders);
        }
    }
}