using RestaurantSys.DTOs.Payment.Request;
using RestaurantSys.Interface;
using RestaurantSys.Models;
using System.Linq;

namespace RestaurantSys.Service
{
    public class PaymentMethodService :IPaymentMethod
    {
        private readonly FoodDeliveryManagementSystemDbContext _context;
        public PaymentMethodService(FoodDeliveryManagementSystemDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddPaymentMethod(AddPaymentMethodDTO input)
        {
            try
            {
                if (input == null)
                    throw new ArgumentNullException(nameof(input));

                bool cardExists = _context.PaymentMethods.Any(x => x.CardNumber == input.CardNumber);
                if (cardExists)
                {
                    throw new Exception("Card Already Exist");
                }

                if (input.ExpiryDate <= DateOnly.FromDateTime(DateTime.Today))
                {
                    return "Card is expired";
                }

                
                var newEntity = new PaymentMethod
                    {
                        CardType = input.CardType,
                        CardNumber = input.CardNumber,
                        Cvc = input.Cvc,
                        ExpiryDate = input.ExpiryDate,
                        UserId = input.UserId

                    };

                    await _context.PaymentMethods.AddAsync(newEntity);
                    await _context.SaveChangesAsync();
                
                




                    return "Payment card created successfully.";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
