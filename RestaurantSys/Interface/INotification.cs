using RestaurantSys.DTOs.Notification.Response;

namespace RestaurantSys.Interface
{
    public interface INotification
    {
        public Task<List<GetNotificationOutputDTO>> GetAllNotification(int userID);
    }
}
