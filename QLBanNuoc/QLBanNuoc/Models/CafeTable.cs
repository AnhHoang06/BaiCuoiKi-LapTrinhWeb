using System.ComponentModel.DataAnnotations;

namespace QLBanNuoc.Models
{
    public class CafeTable
    {
        public int Id { get; set; }

        [Required]
        public int TableNumber { get; set; }

        [Required]
        public int Capacity { get; set; } = 2; // 2 hoặc 4

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "Trong"; // Trong / DangSuDung

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}