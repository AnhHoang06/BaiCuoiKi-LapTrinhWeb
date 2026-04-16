using QLBanNuoc.Models;

namespace QLBanNuoc.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
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
        }
    }
}