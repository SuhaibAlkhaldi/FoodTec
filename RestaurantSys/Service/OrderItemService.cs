using Microsoft.EntityFrameworkCore;
using RestaurantSys.DTOs.OrderItem.Request;
using RestaurantSys.DTOs.OrderItem.Response;
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

       

        public async Task<List<GetCartDTO>> GetCurrentCart(int UserID)
        {
            try
            {
                bool userExists =  _context.Users.Any(u => u.Id == UserID);
                if (userExists == false)
                {
                    throw new Exception("User Not Found");
                }
                else
                {
                    var result = await (
                        from item in _context.Items
                        join orderItem in _context.OrderItems on item.Id equals orderItem.ItemId
                        where orderItem.UserID == UserID
                        select new GetCartDTO
                        {
                            Id = orderItem.Id,
                            ItemNameEn = item.NameEn,
                            ItemNameAr = item.NameAr,
                            DescriptionAr = item.DescriptionAr,
                            DescriptionEn = item.DescriptionEn,
                            Quantity = orderItem.Quantity

                        }).ToListAsync();
                    return result;

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<string> AddOneToCart(AddToCart input)
        {
            OrderItem orderItem = new OrderItem();
            try
            {
                var item = await _context.OrderItems
         .FirstOrDefaultAsync(x => x.UserID == input.UserID && x.ItemId == input.ItemID);

                if (item == null)
                {
                    item = new OrderItem
                    {
                        UserID = input.UserID,
                        ItemId = input.ItemID,
                        Quantity = 1
                    };
                    _context.OrderItems.Add(item);
                }
                else
                {
                    item.Quantity += 1;
                    _context.OrderItems.Update(item);
                }

                await _context.SaveChangesAsync();
                return "Item added to cart";


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> RemoveAll(int userID , int itemID)
        {
            try
            {
                var item = await _context.OrderItems.Where(x => x.UserID == userID && x.ItemId == itemID).FirstOrDefaultAsync();

                if (item == null)
                    throw new Exception("Item not found in cart.");

                _context.OrderItems.Remove(item);
                await _context.SaveChangesAsync();
                return "Item removed from cart.";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> RemoveOne(int userId, int itemId)
        {
            try
            {
                var item = await _context.OrderItems.Where(x => x.UserID == userId && x.ItemId == itemId).FirstOrDefaultAsync();
                if (item == null)
                    throw new Exception("Item not found in cart.");
                item.Quantity -= 1;

                if (item.Quantity <= 0)
                    _context.OrderItems.Remove(item);
                else
                    _context.OrderItems.Update(item);

                await _context.SaveChangesAsync();
                return "One item removed from cart.";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
