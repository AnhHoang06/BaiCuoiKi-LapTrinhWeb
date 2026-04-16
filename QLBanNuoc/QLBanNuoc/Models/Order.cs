using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QLBanNuoc.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên khách hàng không được để trống")]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn loại đơn")]
        [StringLength(20)]
        public string OrderType { get; set; } = "TaiQuan";
        // TaiQuan / MangDi / GiaoHang

        public int? TableId { get; set; }

        [StringLength(255)]
        public string? DeliveryAddress { get; set; }

        [StringLength(100)]
        public string? ReceiverName { get; set; }

        [StringLength(500)]
        public string? Note { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal TotalPrice { get; set; } = 0;

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "ChoXacNhan";
        // ChoXacNhan / DaXacNhan / DangPhaChe / HoanThanh / DaHuy

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public CafeTable? CafeTable { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}