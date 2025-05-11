namespace RestaurantSys.DTOs.Item.Response
{
    public class GetTopRecommendedItemDTO
    {
        public int Id { get; set; }
        public string? NameEn { get; set; }

        public string? NameAr { get; set; }

        public string? DescriptionEn { get; set; }

        public string? DescriptionAr { get; set; }

        public decimal? Price { get; set; }

        public string? Image { get; set; }
    }
}
