using QLBanNuoc.Models;

namespace QLBanNuoc.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            // ================= ADMIN =================
            if (!context.Admins.Any(a => a.Username == "admin"))
            {
                context.Admins.Add(new Admin
                {
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                    FullName = "Quản trị viên"
                });

                context.SaveChanges();
            }

            // ================= CATEGORY =================
            if (!context.Categories.Any())
            {
                context.Categories.AddRange(
                    new Category { Name = "Trà sữa" },
                    new Category { Name = "Trà trái cây" },
                    new Category { Name = "Cà phê" },
                    new Category { Name = "Đá xay" }
                );

                context.SaveChanges();
            }

            // ================= BÀN =================
            if (!context.CafeTables.Any())
            {
                context.CafeTables.AddRange(
                    new CafeTable { TableNumber = 1, Capacity = 2, Status = "Trong" },
                    new CafeTable { TableNumber = 2, Capacity = 2, Status = "Trong" },
                    new CafeTable { TableNumber = 3, Capacity = 2, Status = "Trong" },
                    new CafeTable { TableNumber = 4, Capacity = 2, Status = "Trong" },

                    new CafeTable { TableNumber = 5, Capacity = 4, Status = "Trong" },
                    new CafeTable { TableNumber = 6, Capacity = 4, Status = "Trong" },
                    new CafeTable { TableNumber = 7, Capacity = 4, Status = "Trong" },
                    new CafeTable { TableNumber = 8, Capacity = 4, Status = "Trong" }
                );

                context.SaveChanges();
            }
        }
    }
}