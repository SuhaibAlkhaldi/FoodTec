using RestaurantSys.DTOs.Order.Response;
using RestaurantSys.Models;

namespace RestaurantSys.Interface
{
    public interface IOrder
    {
        public Task<List<GetOrderHistory>> GetOrderHistory(int userID);
    }
}
