using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace RestaurantSys.Helpers.Validation
{
    public static class Validation
    {
        public static bool IsFirstNameValid(string firstName)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrWhiteSpace(firstName) || firstName.Length > 50)
                throw new Exception("First Name is required and should not be more than 50 characters.");

            foreach (char c in firstName)
            {
                if (char.IsDigit(c))
                    throw new Exception("First Name should not contain numbers.");

                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                    throw new Exception("First Name should only contain letters and white spaces.");
            }

            return true;
        }
        public static bool IsLastNameValid(string lastName)
        {
            if (string.IsNullOrEmpty(lastName) || string.IsNullOrWhiteSpace(lastName) || lastName.Length > 50)
                throw new Exception("Last Name is required and should not be more than 50 characters.");

            foreach (char c in lastName)
            {
                if (!char.IsLetter(c) && !char.IsWhiteSpace(c))
                    throw new Exception("Name should only contain letters and white spaces. No numbers or special characters allowed.");
            }

            return true;
        }
        public static bool IsUsernameValid(string username)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username) )
                throw new Exception("username Is Required" );
            if (username.Length < 5 || username.Length > 20)
                throw new Exception("The username must greater than 5 characters and not exceed 20 characters");
            foreach (char ch in username)
            {
                if (!(char.IsLetterOrDigit(ch) || ch == '_'))
                    throw new Exception("Username can only contain letters, numbers, and underscores.");
            }
            return true;
        }
        public static bool IsEmailValid(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Email is required.");

            int atIndex = email.IndexOf('@');
            int dotIndex = email.LastIndexOf('.');

            if (atIndex < 1 || dotIndex < atIndex + 2 || dotIndex >= email.Length - 2)
                throw new Exception("Email format is invalid.");

            string domain = email.Substring(atIndex + 1).ToLower(); 

            
            string[] allowedDomains = { "gmail.com", "hotmail.com", "outlook.com", "zoho.com" };

            if (!allowedDomains.Contains(domain))
                throw new Exception("Only Gmail, Hotmail, Outlook, or Zoho emails are allowed.");

            string localPart = email.Substring(0, atIndex);

            foreach (char c in localPart)
            {
                if (!char.IsLetterOrDigit(c) && c != '.' && c != '_' && c != '%' && c != '+' && c != '-')
                    throw new Exception("Email contains invalid characters.");
            }

            return true;
        }

        public static bool IsPhoneNumberValid(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
                throw new Exception("Phone number is required.");

            if (phoneNumber.Length != 10)
                throw new Exception("Phone number must be exactly 10 digits.");

            if (!phoneNumber.StartsWith("077") &&
                !phoneNumber.StartsWith("078") &&
                !phoneNumber.StartsWith("079"))
                throw new Exception("Phone number must start with 077, 078, or 079.");

            if (!phoneNumber.All(char.IsDigit))
                throw new Exception("Phone number must contain only digits.");

            return true;
        }
        public static bool IsPasswordValid(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required.");

            if (password.Length < 8)
                throw new Exception("Password must be at least 8 characters long.");

            if (!password.Any(char.IsUpper))
                throw new Exception("Password must contain at least one uppercase letter.");

            if (!password.Any(char.IsLower))
                throw new Exception("Password must contain at least one lowercase letter.");

            if (!password.Any(char.IsDigit))
                throw new Exception("Password must contain at least one number.");

            if (!password.Any(c => "!@#$%^&*()_+-=[]{}|;:'\",.<>?/`~".Contains(c)))
                throw new Exception("Password must contain at least one special character.");

            return true;
        }

        public static bool IsImageValid(string image)
        {

            if (string.IsNullOrWhiteSpace(image))
                return true;

            string extension = Path.GetExtension(image).ToLower();


            string[] allowedExtensions = { ".png", ".jpg", ".webp" };

            if (!allowedExtensions.Contains(extension))
                throw new Exception("Only PNG, JPG, and WEBP image formats are allowed.");

            return true;
        }


        public static bool IsValidBirthDate(DateTime birthDate, int minimumAge = 13)
        {
            
            if (birthDate == default)
                throw new Exception("Birthdate is required.");

            
            if (birthDate > DateTime.Today)
                throw new Exception("Birthdate cannot be in the future.");

            
            var today = DateTime.Today;
            int age = today.Year - birthDate.Year;

           
            if (birthDate > today.AddYears(-age))
                age--;

            if (age < minimumAge)
                throw new Exception($"User must be at least {minimumAge} years old.");

            return true;
        }
    }
}
