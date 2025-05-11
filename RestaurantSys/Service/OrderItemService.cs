using Microsoft.EntityFrameworkCore;
using RestaurantSys.DTOs.OrderItem.Request;
using RestaurantSys.Interface;
using RestaurantSys.Models;

namespace RestaurantSys.Service
{
    public class OrderItemService :IOrderItem
    {
        private readonly FoodDeliveryManagementSystemDbContext _context;
        public OrderItemService(FoodDeliveryManagementSystemDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddItemToCart(AddOrderItemDTO input)
        {
            try
            {
                var userExists = await _context.Users.AnyAsync(u => u.Id == input.userID);
                if (!userExists)
                    return "User not found.";

                var itemExists = await _context.Items.AnyAsync(i => i.Id == input.ItemId);
                if (!itemExists)
                    return "Item not found.";

                //var cartExists = await _context.OrderItems.AnyAsync(c => c.Id == input.orderItemID);
                //if (!cartExists)
                //    return "OrderItem not found.";

                var query = await (from user in _context.Users
                                   join item in _context.Items on 1 equals 1
                                   where user.Id == input.userID && item.Id == input.ItemId
                                   select new
                                   {
                                       User = user,
                                       Item = item,
                                       ExistingOrderItem = _context.OrderItems
                                           .FirstOrDefault(x => user.Id == input.userID && x.ItemId == input.ItemId)
                                   }).FirstOrDefaultAsync();
                if (query == null)
                {
                    return "User or Item not found.";
                }

                var newOrderItem = new OrderItem
                {
                    UserID = input.userID,
                    ItemId = input.ItemId,
                    Quantity = input.Quantity
                    //Id = input.orderItemID
                };

                await _context.OrderItems.AddAsync(newOrderItem);
                await _context.SaveChangesAsync();
                return "Added Successfully";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
