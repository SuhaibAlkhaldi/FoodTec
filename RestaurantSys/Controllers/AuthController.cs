using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using RestaurantSys.DTOs.ResetPassword.Request;
using RestaurantSys.DTOs.SignIn.Request;
using RestaurantSys.DTOs.SignIn.Response;
using RestaurantSys.DTOs.SignUp.Request;
using RestaurantSys.DTOs.VerifyOTP.Request;
using RestaurantSys.Helpers.Email;
using RestaurantSys.Helpers.Hashing;
using RestaurantSys.Helpers.Token;
using RestaurantSys.Helpers.Validation;
using RestaurantSys.Models;
using System.Data;
using System.Xml;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Net.WebRequestMethods;

namespace RestaurantSys.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpDTO input)
        {

            var message = " Account Created Successfully";
            try
            {
                //var hashPassword = HashingHelper.HashValueWith384(input.Password);
                if (Validation.IsFirstNameValid(input.FirstName) && Validation.IsLastNameValid(input.LastName) && Validation.IsUsernameValid(input.Username) && Validation.IsPhoneNumberValid(input.PhoneNumber) && Validation.IsPasswordValid(input.Password) && Validation.IsEmailValid(input.Email) && Validation.IsImageValid(input.ProfileImage))
                {
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
                        command.Parameters.AddWithValue("@ProfileImage", input.ProfileImage ?? (object)DBNull.Value);
                        command.Parameters.AddWithValue("@JoinDate", input.JoinDate);
                        command.Parameters.AddWithValue("@Birthdate", input.BirthDate);
                        connection.Open();
                        await command.ExecuteNonQueryAsync();
                        connection.Close();
                    }
                    await EmailHelper.SendEmail(input.Email, input.OTP, "Sign Up OTP", "Complete SignUp");

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
        public async Task<IActionResult> SendOTP(string email)
        {
            try
            {
                if (Validation.IsEmailValid(email))
                {
                    string otp = new Random().Next(1000, 9999).ToString();
                    DateTime otpExpiry = DateTime.Now.AddMinutes(10);
                    var connectionString = _configuration.GetConnectionString("DefaultConnection");
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        SqlCommand command = new SqlCommand("SendOtpToEmail", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@OtpCode", otp);
                        command.Parameters.AddWithValue("@OtpExpiry", otpExpiry);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                    await EmailHelper.SendEmail(email , otp , "Reset Password OTP" , "Complete Reset Password");
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
                    input.Password = HashingHelper.HashValueWith384( input.Password);
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
                return Ok("Reset Password Successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
