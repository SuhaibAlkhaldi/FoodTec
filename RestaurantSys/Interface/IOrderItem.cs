using RestaurantSys.DTOs.OrderItem.Request;
using RestaurantSys.DTOs.OrderItem.Response;

namespace RestaurantSys.Interface
{
    public interface IOrderItem
    {
        public Task<string> AddItemToCart(AddOrderItemDTO input);
        public Task<List<GetCartDTO>> GetCurrentCart(int UserID);

        public Task<string> AddOneToCart(AddToCart input);
        public Task<string> RemoveAll(int userID, int itemID);
        public Task<string> RemoveOne(int userId, int itemId);
    }
}
