namespace Coding_Tracker
{
    public class DateTimeValidator
    {
        public static DateTime ValidateDateResponse(string input, string format = "dd-MM-yyyy HH:mm")
        {
            if (DateTime.TryParseExact(input, format, null, System.Globalization.DateTimeStyles.None, out var result))
            {
                return result;
            }
            throw new FormatException($"Input '{input}' is not in the expected format '{format}'.");
        }
    }
}
