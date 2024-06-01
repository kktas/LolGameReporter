namespace Common
{
    public static class StringUtilities
    {
        public static string? GetFullName(string? firstName, string? lastName)
        {
            string? result;
            if (firstName != null)
            {
                if (lastName != null) { result = $"{firstName} {lastName}"; }
                else { result = $"{firstName}"; }
            }
            else { result = null; }

            return result;
        }
    }
}
