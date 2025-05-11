using RestaurantSys.Interface;
using RestaurantSys.Models;

namespace RestaurantSys.Service
{
    public class FavoriteService : IFavorite
    {
        private readonly FoodDeliveryManagementSystemDbContext _context;
        public FavoriteService(FoodDeliveryManagementSystemDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddToFavorite(int userID, int itemID)
        {
            try
            {
               
                var result = _context.Favorites.Where(f => f.UserId == userID && f.ItemId == itemID).FirstOrDefault();
                if (result != null)
                {
                    return "The Item Is Already Exist";
                }
                var favorite = new Favorite
                {
                    UserId = userID,
                    ItemId = itemID
                };
                    
                _context.Favorites.Add(favorite);
                _context.SaveChanges();
                return "Added Successfully"; 
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<string> RemoveFromFavorite(int userID, int itemID)
        {
            try
            {
                var result = _context.Favorites.Where(f => f.UserId == userID && f.ItemId == itemID).FirstOrDefault();
                if (result == null)
                {
                    return "The Item Is Not Found";
                }
                
                _context.Favorites.Remove(result);
                _context.SaveChanges();
                return "Deleted Successfully";

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
