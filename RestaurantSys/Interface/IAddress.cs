using RestaurantSys.DTOs.Address.Request;

namespace RestaurantSys.Interface
{
    public interface IAddress
    {
        public Task<string> AddNewAddress(AddNewAddressDTO input);
    }
}
