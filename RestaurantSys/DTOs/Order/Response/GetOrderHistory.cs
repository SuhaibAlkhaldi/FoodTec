namespace RestaurantSys.DTOs.Order.Response
{
    public class GetOrderHistory
    {
        public int Id { get; set; }
        public decimal? Latitude { get; set; }

        public decimal? Longitude { get; set; }
        public DateTime CreatedAt { get; set; }
        public float TotalPrice { get; set; }
    }
}
