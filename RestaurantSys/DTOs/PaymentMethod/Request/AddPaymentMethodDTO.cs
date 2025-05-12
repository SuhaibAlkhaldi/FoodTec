namespace RestaurantSys.DTOs.Payment.Request
{
    public class AddPaymentMethodDTO
    {


        public int UserId { get; set; }

        public string CardType { get; set; } = null!;

        public string CardNumber { get; set; } = null!;

        public DateOnly ExpiryDate { get; set; }

        public string Cvc { get; set; } = null!;
    }
}
