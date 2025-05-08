namespace RestaurantSys.DTOs.Offer.Response
{
    public class GetAllOfferOutputDTO
    {
        public int Id { get; set; }

        public string? TitleEn { get; set; }

        public string? TitleAr { get; set; }

        public string? DescriptionEn { get; set; }

        public string? DescriptionAr { get; set; }
        public string? Image { get; set; }
    }
}
