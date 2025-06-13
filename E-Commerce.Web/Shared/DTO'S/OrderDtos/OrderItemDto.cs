namespace Shared.DTO_S.OrderDtos
{
    public class OrderItemDto
    {
        public string ProductName { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public decimal Price { get; set; }
        public int Quentity { get; set; }
    }
}