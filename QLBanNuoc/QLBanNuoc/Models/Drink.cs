using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLBanNuoc.Models
{
    public class Drink
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên món không được để trống")]
        [StringLength(150)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Giá không được để trống")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn loại món")]
        public int CategoryId { get; set; }

        [StringLength(255)]
        public string? ImageUrl { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public bool IsAvailable { get; set; } = true;

        public Category? Category { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}