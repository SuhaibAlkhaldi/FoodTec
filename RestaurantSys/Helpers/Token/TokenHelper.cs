using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RestaurantSys.Helpers.Token
{
    public static class TokenHelper
    {
        public static string GenerateJWTToken(int userID, int roleId, string username )
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            string secret = "LongPrimarySecretForCapstoneProject";
            var tokenBytesKey = Encoding.UTF8.GetBytes(secret);
            var descriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(10),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserID" , userID.ToString()),
                    new Claim("RoleId" , roleId.ToString()),
                    new Claim("UserName", username)



                }),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenBytesKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenJson = jwtTokenHandler.CreateToken(descriptor);
            var token = jwtTokenHandler.WriteToken(tokenJson);
            
            return token;
        }


        public static string IsValidToken(string token)
        {
            try
            {
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                string secret = "LongPrimarySecretForCapstoneProject";
                var tokenBytesKey = Encoding.UTF8.GetBytes(secret);
                var tokenValidator = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(tokenBytesKey),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                var tokenBase = jwtTokenHandler.ValidateToken(token, tokenValidator, out SecurityToken validateToken);
                return "Valid";
            }
            catch (Exception ex)
            {
                return $"Invalid {ex.Message}";
            }
            
        }

    }
}
