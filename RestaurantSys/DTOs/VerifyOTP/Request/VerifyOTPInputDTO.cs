namespace RestaurantSys.DTOs.VerifyOTP.Request
{
    public class VerifyOTPInputDTO
    {
        public string Email { get; set; }
        public string OTP_Code { get; set; }
        public bool IsSignup { get; set; }
    }
}
