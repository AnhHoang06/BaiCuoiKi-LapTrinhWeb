using QLBanNuoc.Models;

namespace QLBanNuoc.ViewModels
{
    public class HomeIndexViewModel
    {
        public List<Drink> Drinks { get; set; } = new();
        public List<Category> Categories { get; set; } = new();
    }

    // DTO nhận JSON từ trang giỏ hàng
    public class PlaceOrderRequest
    {
        public string CustomerName { get; set; } = "";
        public string Phone { get; set; } = "";
        public string OrderType { get; set; } = "TaiQuan";
        public int? TableNumber { get; set; }
        public string? DeliveryAddress { get; set; }
        public string? Note { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
    }

    public class OrderItemDto
    {
        public int DrinkId { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
