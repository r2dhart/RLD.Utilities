using System;
using System.Text;

namespace RLD.Utilities.Extensions
{
    /// <summary>
    /// BooleanFormatInfo Enum
    /// 
    /// Contains the extension methods I have developed to add onto the System.Boolean library.
    /// </summary>
    public enum BooleanFormatInfo
    {
        OneZero,
        YesNo,
        YupNope,
        YN,
        TrueFalse,
        TF,
        PassFail,
    }

    /// <summary>
    /// BooleanExtensions Class
    /// 
    /// Extension methods to be used with boolean data types.
    /// </summary>
    public static class BooleanExtensions
    {
        public static object Emum { get; private set; }

        /// <summary>
        /// Converts a boolean value to a string indicating it's displayed value.
        /// </summary>
        /// <param name="value">The boolean value to be evaluated.</param>
        /// <param name="format">The format to be applied.</param>
        /// <returns>A string corresponding to the format selected and the value evelauated.</returns>
        /// Contributed by Russell Dehart
        public static string ToString(this bool value, BooleanFormatInfo format)
        {
            string formatString = Enum.GetName(format.GetType(), format);
            return ParseBooleanString(value, formatString);
        }

        // Parses the values from the enum text
        private static string ParseBooleanString(bool value, string formatString)
        {
            StringBuilder tResult = new StringBuilder();
            StringBuilder fResult = new StringBuilder();

            int charCount = formatString.Length;

            bool isTrueString = true;

            for (int i = 0; i != charCount; i++)
            {
                if (char.IsUpper(formatString[i]) && i != 0) isTrueString = false;

                if (isTrueString)
                    tResult.Append(formatString[i]);
                else
                    fResult.Append(formatString[i]);
            }

            return (value == true ? tResult.ToString() : fResult.ToString());
        }
    }
}
