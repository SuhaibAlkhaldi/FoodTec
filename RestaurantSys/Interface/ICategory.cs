using RestaurantSys.DTOs.Category.Response;

namespace RestaurantSys.Interface
{
    public interface ICategory
    {
        public Task<List<GetAllCategoryOutputDTO>> GetAllActiveCategory();
    }
}
