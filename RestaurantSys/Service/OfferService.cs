using RestaurantSys.DTOs.Offer.Response;
using RestaurantSys.Interface;
using RestaurantSys.Models;

namespace RestaurantSys.Service
{
    public class OfferService : IOffer
    {
        private readonly FoodDeliveryManagementSystemDbContext _context;
        public OfferService(FoodDeliveryManagementSystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<GetAllOfferOutputDTO>> GetAllActiveOffer()
        {
            try
            {
                var getOffer = _context.Offers.Where(x => x.IsActive ==  true).ToList();
                var offer = getOffer.Select(o => new GetAllOfferOutputDTO
                {
                    Id = o.Id,
                    TitleAr = o.TitleAr,
                    TitleEn = o.TitleEn,
                    DescriptionAr = o.DescriptionAr,
                    DescriptionEn = o.DescriptionEn,
                    Image = o.Image
                }).ToList();
                return offer ;
            }
            catch (Exception ex)
            {
                throw new Exception("SomeThing Went Wrong");
            }
        }
    }
}
