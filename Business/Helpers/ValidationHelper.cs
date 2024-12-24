using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

// This class was written by ChatGPT.
// It provides methods to validate email addresses and phone numbers.
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
    }
}
