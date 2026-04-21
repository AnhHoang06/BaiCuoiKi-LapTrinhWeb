using QLBanNuoc.Models;

namespace QLBanNuoc.Data
{
    public static class SampleSeed
    {
        public static void Run(AppDbContext context)
        {
            if (!context.Drinks.Any())
            {
                var traSua = context.Categories.FirstOrDefault(c => c.Name == "Trà sữa");
                var traTraiCay = context.Categories.FirstOrDefault(c => c.Name == "Trà trái cây");
                var caPhe = context.Categories.FirstOrDefault(c => c.Name == "Cà phê");
                var daXay = context.Categories.FirstOrDefault(c => c.Name == "Đá xay");

                if (traSua == null || traTraiCay == null || caPhe == null || daXay == null)
                    return;

                context.Drinks.AddRange(
                    new Drink
                    {
                        Name = "Trà sữa truyền thống",
                        Price = 25000,
                        CategoryId = traSua.Id,
                        ImageUrl = "/images/Item/TraSuaTruyenThong.jpg",
                        Description = "Best seller",
                        IsAvailable = true
                    },
                    new Drink
                    {
                        Name = "Trà đào cam sả",
                        Price = 30000,
                        CategoryId = traTraiCay.Id,
                        ImageUrl = "",
                        Description = "Mát lạnh",
                        IsAvailable = true
                    },
                    new Drink
                    {
                        Name = "Cà phê sữa đá",
                        Price = 22000,
                        CategoryId = caPhe.Id,
                        ImageUrl = "",
                        Description = "Đậm vị Việt Nam",
                        IsAvailable = true
                    },
                    new Drink
                    {
                        Name = "Matcha đá xay",
                        Price = 35000,
                        CategoryId = daXay.Id,
                        ImageUrl = "",
                        Description = "Thơm béo",
                        IsAvailable = true
                    }
                );

                context.SaveChanges();
            }
        }
    }
}