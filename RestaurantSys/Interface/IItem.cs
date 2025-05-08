using RestaurantSys.DTOs.Item.Response;

namespace RestaurantSys.Interface
{
    public interface IItem
    {
        public Task<List<GetItemOutputDTO>> GetTopRatedItem();
        public Task<GetItemOutputDTO> GetItemByCategoryID(int categoryID);
        public Task<List<FavoriteItemOutputDTO>> GetFavoriteItem(int userID);
        public Task<List<GetItemOutputDTO>> GetItemById(int id);
        
    }
}
