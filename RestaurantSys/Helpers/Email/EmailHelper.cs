using SendGrid.Helpers.Mail;
using SendGrid;

namespace RestaurantSys.Helpers.Email
{
    public static class EmailHelper
    { 
        private static readonly string apiKey = ""; 
        private static readonly EmailAddress sender = new EmailAddress("suhaibamjad73@gmail.com", "FoodTec Admin");

        // For account verification email
        public static async Task SendVerificationEmail(string email, string subject, string verificationLinkHtml)
        {
            await SendEmailInternal(email, subject, verificationLinkHtml);
        }

        // For OTP email
        public static async Task SendOTPEmail(string email, string otpCode, string subject, string message)
        {
            string htmlBody = $"{message}<br/><strong>Your OTP Code:</strong> {otpCode}";
            await SendEmailInternal(email, subject, htmlBody);
        }

        // Shared internal method
        private static async Task SendEmailInternal(string email, string subject, string htmlMessage)
        {
            var client = new SendGridClient(apiKey);
            var to = new EmailAddress(email, "FoodTec");
            var plainTextContent = htmlMessage;

            var msg = MailHelper.CreateSingleEmail(sender, to, subject, plainTextContent, htmlMessage);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
