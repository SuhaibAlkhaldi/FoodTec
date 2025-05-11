using RestaurantSys.DTOs.OrderItem.Request;

namespace RestaurantSys.Interface
{
    public interface IOrderItem
    {
        public Task<string> AddItemToCart(AddOrderItemDTO input);
    }
}
