using System.Reflection;

namespace Core.Helpers
{
    public class PropertyLogger
    {
        public static void LogAllProperties(object obj)
        {
            if (obj == null)
            {
                Console.WriteLine("Object is null.");
                return;
            }

            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object? value = property.GetValue(obj);
                Console.WriteLine($"{property.Name}: ({value})");
            }
        }
    }
}
