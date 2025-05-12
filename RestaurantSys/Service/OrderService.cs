using Microsoft.EntityFrameworkCore;
using RestaurantSys.DTOs.Order.Response;
using RestaurantSys.Interface;
using RestaurantSys.Models;

namespace RestaurantSys.Service
{
    public class OrderService : IOrder
    {
        private readonly FoodDeliveryManagementSystemDbContext _context;
        public OrderService(FoodDeliveryManagementSystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetOrderHistory>> GetOrderHistory(int userID)
        {
            try
            {
                var check = _context.Users.Find(userID);
                if (check == null)
                {
                    throw new Exception("User Not Found");
                }
                var query = await(
                    from order in _context.Orders
                    join address in _context.Addresses on order.AddressId equals address.Id
                    where order.UserId == userID
                    select new GetOrderHistory
                    {
                        Id = order.Id,
                        Latitude = address.Latitude,
                        Longitude = address.Longitude,
                        CreatedAt = (DateTime)order.CreatedAt,
                        TotalPrice = (float)order.TotalPrice
                    }).ToListAsync();
                return query;
            }
            catch(Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
