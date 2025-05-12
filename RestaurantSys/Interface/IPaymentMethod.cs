using RestaurantSys.DTOs.Payment.Request;

namespace RestaurantSys.Interface
{
    public interface IPaymentMethod
    {
        public Task<string> AddPaymentMethod(AddPaymentMethodDTO input);
    }
}
