using Microsoft.EntityFrameworkCore;
using RestaurantSys.DTOs.Item.Response;
using RestaurantSys.Interface;
using RestaurantSys.Models;

namespace RestaurantSys.Service
{
    public class ItemService : IItem
    {
        private readonly FoodDeliveryManagementSystemDbContext _context;
        public ItemService(FoodDeliveryManagementSystemDbContext context)
        {
            _context = context;
        }
        public async Task<List<GetItemOutputDTO>> GetTopRatedItem()
        {
            try
            {
                var item =  _context.Items.OrderByDescending(x => x.ItemRate)
                    .Take(10).OrderBy(i => i.ItemRate)
                    .Select(x => new GetItemOutputDTO
                     {
                        Id = x.Id,
                        NameAr = x.NameAr,
                        NameEn = x.NameEn,
                        DescriptionAr = x.DescriptionAr,
                        DescriptionEn = x.DescriptionEn,
                        ItemRate = x.ItemRate,
                        Price = x.Price,
                        Image = x.Image
                    
                     }).ToList();
                return item;
                
            }
            catch (Exception ex)
            {
                throw new Exception("SomeThing Went Wrong");
            }
        }


        public async Task<GetItemOutputDTO> GetItemByCategoryID(int categoryID)
        {
            try
            {
                if (categoryID <= 0)
                {
                    throw new Exception("Invalid Category ID");
                }
                var getItem =  _context.Items.Where(x => x.CategoryId == categoryID).SingleOrDefault();
                if (getItem == null)
                {
                    throw new Exception($"No item found for category ID {categoryID}.");
                }
                var item = new GetItemOutputDTO
                {
                    Id = getItem.Id,
                    NameAr = getItem.NameAr,
                    NameEn = getItem.NameEn,
                    DescriptionAr = getItem.DescriptionAr,
                    DescriptionEn = getItem.DescriptionEn,
                    Image = getItem.Image
                };
                return item;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<List<FavoriteItemOutputDTO>> GetFavoriteItem(int userID)
        {
            try
            {
                if (userID <= 0)
                {
                    throw new Exception("Invalid User ID");
                }
                var result = (
                from fav in _context.Favorites
                join item in _context.Items on fav.ItemId equals item.Id
                where fav.UserId == userID
                select new FavoriteItemOutputDTO
                {
                    Id = item.Id,
                    NameAr = item.NameAr,
                    NameEn = item.NameEn,
                    DescriptionAr = item.DescriptionAr,
                    DescriptionEn = item.DescriptionEn,
                    Price = item.Price,
                    CreationDate = item.CreatedAt
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<GetItemOutputDTO>> GetItemById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new Exception("Invalid Id");
                }
                var getItem = await _context.Items.FindAsync(id);
                if (getItem == null)
                {
                    throw new Exception($"Item with ID {id} was not found.");
                }
                var result = _context.Items.Where(x => x.Id == id).Select(i => new GetItemOutputDTO
                {
                    Id = i.Id,
                    NameAr = i.NameAr,
                    NameEn = i.NameEn,
                    DescriptionAr = i.DescriptionAr,
                    DescriptionEn = i.DescriptionEn,
                    Price = i.Price,
                    ItemRate = i.ItemRate
                }).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public async Task<List<GetTopRecommendedItemDTO>> GetTopRecommendedItem()
        {
            try
            {
                var result = await _context.OrderItems.GroupBy(x => x.ItemId)
                    .OrderByDescending(x => x.Count()).Take(10).Select(g => new GetTopRecommendedItemDTO
                    {
                        Id = g.First().Item.Id,
                        NameEn = g.First().Item.NameEn,
                        NameAr = g.First().Item.NameAr,
                        DescriptionEn = g.First().Item.DescriptionEn,
                        DescriptionAr = g.First().Item.DescriptionAr,
                        Price = g.First().Item.Price,
                        Image = g.First().Item.Image
                    }).ToListAsync();


                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
