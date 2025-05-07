using RestaurantSys.DTOs.Category.Response;
using RestaurantSys.Interface;
using RestaurantSys.Models;

namespace RestaurantSys.Service
{
    public class CategoryService : ICategory
    {
        private readonly FoodDeliveryManagementSystemDbContext _context;
        public CategoryService(FoodDeliveryManagementSystemDbContext context)
        {
            _context = context;
        }
        public async Task<List<GetAllCategoryOutputDTO>> GetAllActiveCategory()
        {
            try
            {
                var getCategory = _context.Categories.Where(x => x.IsActive == true).ToList();
                var category = getCategory.Select(c => new GetAllCategoryOutputDTO
                {
                    Id = c.Id,
                    NameAr = c.NameAr,
                    NameEn = c.NameEn,
                    Image = c.Image
                }).ToList();
                return category;
            }
            catch (Exception ex)
            {
                throw new Exception("SomeThing Went Wrong");
            }
            
        }
    }
}
