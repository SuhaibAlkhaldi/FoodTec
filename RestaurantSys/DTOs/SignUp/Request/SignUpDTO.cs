namespace RestaurantSys.DTOs.SignUp.Request
{
    public  class SignUpDTO
    {
        public int RoleId { get; set; } = 2;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        //public string OTP { get; set; } = null;
        public string? ProfileImage { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
