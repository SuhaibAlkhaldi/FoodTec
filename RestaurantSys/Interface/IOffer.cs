using RestaurantSys.DTOs.Offer.Response;

namespace RestaurantSys.Interface
{
    public interface IOffer
    {
        public Task<List<GetAllOfferOutputDTO>> GetAllActiveOffer();
    }
}
