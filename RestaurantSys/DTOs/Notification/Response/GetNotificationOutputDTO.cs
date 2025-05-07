namespace RestaurantSys.DTOs.Notification.Response
{
    public class GetNotificationOutputDTO
    {
        public int Id { get; set; }
        public string? Title { get; set; }

        public string? Description { get; set; }
        public bool? IsRead { get; set; }
    }
}
