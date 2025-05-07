using RestaurantSys.DTOs.Notification.Response;
using RestaurantSys.Interface;
using RestaurantSys.Models;

namespace RestaurantSys.Service
{
    public class NotificationService : INotification
    {
        private readonly FoodDeliveryManagementSystemDbContext _context;
        public NotificationService(FoodDeliveryManagementSystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetNotificationOutputDTO>> GetAllNotification(int userID)
        {
            try
            {
                if (userID <=0)
                {
                    throw new Exception("Invalid User ID");
                }
                
                var getNotification = _context.Notifications.Where(x => x.UserId == userID).ToList();
                if (getNotification == null)
                {
                    throw new Exception($"No Notification For UserID = {userID}");
                }
                var notification = getNotification.Select(x => new GetNotificationOutputDTO
                    {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    IsRead = x.IsRead
                }).ToList();

                return notification; 
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
