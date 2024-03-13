using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Core.Helpers
{
    public static class StringHelper
    {

        public static string TrimWhitespace(string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    return input;
                }

                // Replace multiple spaces with a single space
                input = Regex.Replace(input, @"\s+", " ");

                // Trim leading and trailing whitespace
                input = input.Trim();


            }
            catch (Exception)
            {
            }
            return input;
        }

        public static List<string> GetKeywordsFromSearchTerm(string searchTerm)
        {
            List<string> keywords = searchTerm.Split(" ").ToList();
            return keywords;
        }

        public static string RefinedSearchTerm(string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    return input;
                }

                // Replace multiple spaces with a single space
                input = Regex.Replace(input, @"\s+", " ");

                // Trim leading and trailing whitespace
                input = input.ToLower().Trim();


            }
            catch (Exception)
            {
            }
            return input;
        }

        public static string RefinedText(string text)
        {
            // Remove diacritics
            string normalized = text.ToLower().Normalize(NormalizationForm.FormD);
            char[] chars = normalized
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray();

            // Convert to lowercase and remove spaces 
            string result = new string(chars).Replace(@"\s+", " ").Trim();

            return result;
        }

        public static T TrimStringProperties<T>(T obj) where T : class
        {
            try
            {
                Type type = obj.GetType();
                PropertyInfo[] properties = type.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    if (property.PropertyType == typeof(string))
                    {
                        string value = (string)property.GetValue(obj);
                        if (value != null)
                        {
                            value = Regex.Replace(value, @"\s+", " ");
                            string trimmedValue = value.Trim();
                            property.SetValue(obj, trimmedValue);
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return obj;
        }

        public static string ConvertWithoutDiacritics(string text)
        {
            // Remove diacritics
            string normalized = text.ToLower().Normalize(NormalizationForm.FormD);
            char[] chars = normalized
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray();

            // Convert to lowercase and remove spaces 
            string result = new string(chars).Replace(" ", "");

            return result;
        }


    }
}


