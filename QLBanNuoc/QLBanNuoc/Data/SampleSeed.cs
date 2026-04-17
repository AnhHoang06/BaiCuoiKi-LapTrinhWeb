using QLBanNuoc.Models;

namespace QLBanNuoc.Data
{
    public static class SampleSeed
    {
        public static void Run(AppDbContext context)
        {
            if (!context.Drinks.Any())
            {
                context.Drinks.AddRange(
                    new Drink
                    {
                        Name = "Trà sữa truyền thống",
                        Price = 25000,
                        CategoryId = 1,
                        ImageUrl = "",
                        Description = "Best seller",
                        IsAvailable = true
                    },

                    new Drink
                    {
                        Name = "Trà đào cam sả",
                        Price = 30000,
                        CategoryId = 2,
                        ImageUrl = "",
                        Description = "Mát lạnh",
                        IsAvailable = true
                    },

                    new Drink
                    {
                        Name = "Cà phê sữa đá",
                        Price = 22000,
                        CategoryId = 3,
                        ImageUrl = "",
                        Description = "Đậm vị Việt Nam",
                        IsAvailable = true
                    },

                    new Drink
                    {
                        Name = "Matcha đá xay",
                        Price = 35000,
                        CategoryId = 4,
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