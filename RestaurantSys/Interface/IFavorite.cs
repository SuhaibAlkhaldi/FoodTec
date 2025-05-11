namespace RestaurantSys.Interface
{
    public interface IFavorite
    {
        public Task<string> AddToFavorite(int userID ,int itemID);
        public Task<string> RemoveFromFavorite(int userID, int itemID);
    }
}
