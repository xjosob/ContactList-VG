using System.Text.RegularExpressions;

// This class was written by ChatGPT.
// It provides methods to validate email addresses, phone numbers and postal code.
// The methods use regular expressions (Regex) to ensure inputs match expected patterns.

namespace Business.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return emailRegex.IsMatch(email);
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return false;

            var phoneRegex = new Regex(@"^(\+46|0)[0-9][0-9\s\-()]{6,13}$");
            return phoneRegex.IsMatch(phoneNumber);
        }

        public static bool IsValidPostalCode(string postalCode)
        {
            if (string.IsNullOrEmpty(postalCode))
                return false;
            var postalCodeRegex = new Regex(@"^\d{3}\s?\d{2}$");
            return postalCodeRegex.IsMatch(postalCode);
        }
    }
}
