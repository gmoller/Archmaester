using System;

namespace GeneralUtilities
{
    public static class StringExtensions
    {
        public static int ToInt32(this string s)
        {
            try
            {
                int i = Convert.ToInt32(s);

                return i;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to convert string [{s}] to Int32.", ex);
            }
        }

        public static float ToFloat(this string s)
        {
            try
            {
                float f = Convert.ToSingle(s);

                return f;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to convert string [{s}] to float.", ex);
            }
        }

        public static T ToEnum<T>(this string value, bool ignoreCase = true)
        {
            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }
    }
}