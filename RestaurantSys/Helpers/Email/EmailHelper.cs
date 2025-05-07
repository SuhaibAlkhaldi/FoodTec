using SendGrid.Helpers.Mail;
using SendGrid;

namespace RestaurantSys.Helpers.Email
{
    public static class EmailHelper
    {
        public static async Task SendEmail(string email , string code , string title , string message)
        {
            var apiKey = "";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("suhaibamjad73@gmail.com", "FoodTec Admin");
            var subject = title;
            var to = new EmailAddress(email, "FoodTec");
            var plainTextContent = $"Dear User {message} Please Use The Following OTP Code {code} It Will Be Expired Within 10 Minutes";
            //var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, "");
            var response = await client.SendEmailAsync(msg);
        }
    }
}
