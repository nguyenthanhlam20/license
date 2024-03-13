using System.ComponentModel.DataAnnotations;

namespace Core.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]

    public class EmailValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is string email)
            {
                if (!IsValidEmail(email))
                {
                    return false;
                }
            }
            return true;
        }

        private bool IsValidEmail(string email)
        {
            return true;
        }
    }
}
