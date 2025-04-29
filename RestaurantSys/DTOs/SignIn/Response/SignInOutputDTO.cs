namespace RestaurantSys.DTOs.SignIn.Response
{
    public class SignInOutputDTO
    {
        public int Id { get; set; }
        public int RoleID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
    }
}
