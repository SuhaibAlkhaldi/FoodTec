using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using RestaurantSys.DTOs.ResetPassword.Request;
using RestaurantSys.DTOs.SendOTP;
using RestaurantSys.DTOs.SendOTP.Request;
using RestaurantSys.DTOs.SignIn.Request;
using RestaurantSys.DTOs.SignIn.Response;
using RestaurantSys.DTOs.SignUp.Request;
using RestaurantSys.DTOs.VerifyOTP.Request;
using RestaurantSys.Helpers.Email;
using RestaurantSys.Helpers.Hashing;
using RestaurantSys.Helpers.Image;
using RestaurantSys.Helpers.Token;
using RestaurantSys.Helpers.Validation;
using RestaurantSys.Models;
using System.Data;
using System.Xml;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.WebRequestMethods;

namespace RestaurantSys.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost("SignUp")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SignUp([FromForm] SignUpDTO input)
        {

            var message = " Account Created Successfully. Check your email to verify your account.";
            try
            {
               
                if (Validation.IsFirstNameValid(input.FirstName) && Validation.IsLastNameValid(input.LastName) && Validation.IsUsernameValid(input.Username) && Validation.IsPhoneNumberValid(input.PhoneNumber) && Validation.IsPasswordValid(input.Password) && Validation.IsEmailValid(input.Email) && Validation.IsImageValid(input.ProfileImage))
                {
                    string? imagePath = null;

                    if (input.ProfileImage != null)
                    {
                        imagePath = await ImageHelper.SaveImageAsync(input.ProfileImage);
                    }

                    string verificationToken = Guid.NewGuid().ToString();

                    var connectionString = _configuration.GetConnectionString("DefaultConnection");
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand("SignUp", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@FirstName", input.FirstName);
                        command.Parameters.AddWithValue("@LastName", input.LastName);
                        command.Parameters.AddWithValue("@Username", input.Username);
                        command.Parameters.AddWithValue("@PhoneNumber", input.PhoneNumber);
                        command.Parameters.AddWithValue("@Password", HashingHelper.HashValueWith384(input.Password));
                        command.Parameters.AddWithValue("@Email", input.Email);
                        command.Parameters.AddWithValue("@RoleId", input.RoleId);
                        command.Parameters.AddWithValue("@ProfileImage", imagePath ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@JoinDate", input.JoinDate);
                        command.Parameters.AddWithValue("@Birthdate", input.BirthDate);
                        command.Parameters.AddWithValue("@VerificationToken", verificationToken);
                        connection.Open();
                        await command.ExecuteNonQueryAsync();
                        connection.Close();
                    }
                    
                    string verificationUrl = $"https://localhost:44354/api/auth/verify?token={verificationToken}";

                    string emailBody = $"<p>Click to verify your account:</p><p>{verificationUrl}</p>";





                    await EmailHelper.SendVerificationEmail(input.Email, "Email Verification", emailBody);

                }
                return StatusCode(201, message);
            }
            catch (Exception ex)
            {
                return StatusCode(400, $"Bad Request: {ex.Message}");
            }

        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] SignInInputDTO input)
        {
            var response = new SignInOutputDTO();
            try
            {
                if (Validation.IsEmailValid(input.Email))
                {
                    var connectionString = _configuration.GetConnectionString("DefaultConnection");
                    using (SqlConnection connection = new SqlConnection(connectionString))
                        
                    {
                        

                        SqlCommand command = new SqlCommand("Signin", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Email", input.Email);
                        command.Parameters.AddWithValue("@Password",HashingHelper.HashValueWith384( input.Password));
                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        if (dt.Rows.Count == 0)
                            throw new Exception("Invalid Email / Password");
                        if (dt.Rows.Count > 1)
                            throw new Exception("Query Contains More Than One Element");
                        foreach (DataRow row in dt.Rows)
                        {
                            response.Id = Convert.ToInt32(row["ID"]);
                            response.FirstName = row["FirstName"].ToString();
                            response.LastName = row["LastName"].ToString();
                            response.RoleID = Convert.ToInt32(row["role_id"]);
                            response.UserName = row["UserName"].ToString();
                            response.IsActive = Convert.ToBoolean(row["is_active"]);
                        }

                        // Generate JWT
                        string token = TokenHelper.GenerateJWTToken(response.Id, response.RoleID, response.UserName);
                        response.Token = token;


                    }

                }
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



        [HttpPost("SendOTP")]
        public async Task<IActionResult> SendOTP([FromBody] SendOTPRequestDTO input)
        {
            try
            {
                if (Validation.IsEmailValid(input.Email))
                {
                    string otp = new Random().Next(1000, 9999).ToString();
                    DateTime otpExpiry = DateTime.Now.AddMinutes(10);
                    var connectionString = _configuration.GetConnectionString("DefaultConnection");
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand("SendOtpToEmail", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Email", input.Email );
                        command.Parameters.AddWithValue("@OtpCode", otp);
                        command.Parameters.AddWithValue("@OtpExpiry", otpExpiry);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    await EmailHelper.SendOTPEmail(input.Email, otp, "Reset Password OTP", "Use the code below to reset your password:");
                }
                return Ok("OTP sent to email successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("VerifyOTP")]
        public async Task<IActionResult> VerifyOTP([FromBody] VerifyOTPInputDTO input)
        {
            
            var result = 0;
            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand("VerifyOtpCode", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Email", input.Email);
                    command.Parameters.AddWithValue("@OtpCode", input.OTP_Code);
                    // var userID = command.Parameters.Add("@UserId");
                    //var roleName = command.Parameters.Add("@RoleName");
                    connection.Open();
                    result = (int)command.ExecuteScalar();
                    connection.Close();
                    //var response = TokenHelper.GenerateJWTToken(userID);
                }

                if (result == 1)
                {
                    return Ok("OTP Verified Successfully");
                }
                else
                {
                    return StatusCode(400, "Wrong OTP");
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }



            [HttpPut("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordInputDTO input)
        {
            
            try
            {

                if (Validation.IsEmailValid(input.Email) && Validation.IsPasswordValid(input.Password))
                {
                    if (input.Password == input.confirmPassword)
                    {
                        input.Password = HashingHelper.HashValueWith384(input.Password);
                        var connectionString = _configuration.GetConnectionString("DefaultConnection");
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            SqlCommand command = new SqlCommand("ResetUserPassword", connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@Email", input.Email);
                            command.Parameters.AddWithValue("@NewPassword", input.Password);
                            connection.Open();
                            command.ExecuteNonQuery();
                            connection.Close();
                        }

                    }
                    else
                    {
                        throw new Exception("The passwords not mathched!");
                    }
                    
                }
                return Ok("Reset Password Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("verify")]
        public async Task<IActionResult> VerifyEmail([FromQuery] string token)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();

            var command = new SqlCommand("UPDATE [User] SET is_verified = 1, verification_token = NULL WHERE verification_token = @Token", connection);
            command.Parameters.AddWithValue("@Token", token);

            int rowsAffected = await command.ExecuteNonQueryAsync();
            return rowsAffected > 0
                ? Ok("Email verified successfully.")
                : BadRequest("Invalid or expired token.");
        }

    }
}
