namespace QLBanNuoc.Models
{
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