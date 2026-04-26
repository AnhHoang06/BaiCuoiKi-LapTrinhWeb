using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace QLBanNuoc.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<CafeTable> CafeTables { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Admin> Admins { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.HasIndex(c => c.Name).IsUnique();
            });

            modelBuilder.Entity<Drink>(entity =>
            {
                entity.ToTable("Drink");

                entity.Property(d => d.Price)
                      .HasColumnType("decimal(10,2)");

                entity.HasOne(d => d.Category)
                      .WithMany(c => c.Drinks)
                      .HasForeignKey(d => d.CategoryId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasCheckConstraint("CK_Drink_Price", "[Price] >= 0");
            });

            modelBuilder.Entity<CafeTable>(entity =>
            {
                entity.ToTable("CafeTable");

                entity.HasIndex(t => t.TableNumber).IsUnique();

                entity.HasCheckConstraint("CK_CafeTable_Capacity", "[Capacity] IN (2,4)");
                entity.HasCheckConstraint("CK_CafeTable_Status", "[Status] IN (N'Trong', N'DangSuDung')");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Orders");

                entity.Property(o => o.TotalPrice)
                      .HasColumnType("decimal(10,2)");

                entity.HasOne(o => o.CafeTable)
                      .WithMany(t => t.Orders)
                      .HasForeignKey(o => o.TableId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasCheckConstraint("CK_Order_OrderType", "[OrderType] IN (N'TaiQuan', N'MangDi', N'GiaoHang')");
                entity.HasCheckConstraint("CK_Order_Status", "[Status] IN (N'ChoXacNhan', N'DaXacNhan', N'DangPhaChe', N'HoanThanh', N'DaHuy')");
                entity.HasCheckConstraint("CK_Order_TotalPrice", "[TotalPrice] >= 0");
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.ToTable("OrderItem");

                entity.Property(oi => oi.Price)
                      .HasColumnType("decimal(10,2)");

                entity.HasOne(oi => oi.Order)
                      .WithMany(o => o.OrderItems)
                      .HasForeignKey(oi => oi.OrderId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(oi => oi.Drink)
                      .WithMany(d => d.OrderItems)
                      .HasForeignKey(oi => oi.DrinkId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(oi => new { oi.OrderId, oi.DrinkId }).IsUnique();

                entity.HasCheckConstraint("CK_OrderItem_Quantity", "[Quantity] > 0");
                entity.HasCheckConstraint("CK_OrderItem_Price", "[Price] >= 0");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.HasIndex(a => a.Username).IsUnique();
            });
        }
    }
}