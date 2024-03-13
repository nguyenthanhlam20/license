namespace Core.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime RoundToMinute(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0);
        }
    }
}
