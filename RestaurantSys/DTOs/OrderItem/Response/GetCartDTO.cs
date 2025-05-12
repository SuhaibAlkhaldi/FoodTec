namespace RestaurantSys.DTOs.OrderItem.Response
{
    public class GetCartDTO
    {
        public int Id { get; set; }
        
        public int Quantity { get; set; }
        public string? ItemNameEn { get; set; }

        public string? ItemNameAr { get; set; }

        public string? DescriptionEn { get; set; }

        public string? DescriptionAr { get; set; }
    }
}
