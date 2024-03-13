using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Core.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public class PhoneNumberValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string phoneNumber)
            {
                if (!IsVietnamesePhoneNumber(phoneNumber))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsVietnamesePhoneNumber(string phoneNumber)
        {
            phoneNumber = Regex.Replace(phoneNumber, @"\s", "");
            string pattern = @"^0\d{9,12}$|^\+84\d{9,12}$";

            return System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, pattern);
        }
    }
}
