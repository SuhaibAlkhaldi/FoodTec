namespace RestaurantSys.DTOs.ResetPassword.Request
{
    public class ResetPasswordInputDTO
    {
        public string Password { get; set; }
        public string confirmPassword { get; set; }
        public string Email { get; set; }
    }
}
