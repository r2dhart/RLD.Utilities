using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace RLD.Utilities.Extensions
{
    /// <summary>
    /// StringExtensions Class
    /// 
    /// Contains the extension methods I have developed to add onto the System.String library.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Determines if a string contains an empty character
        /// </summary>
        /// <param name="s">The string to be evaluated.</param>
        /// <returns>A boolean indicating if spaces are present in the string.</returns>
        /// Contributed by Russell Dehart
        public static bool ContainsSpace(this string s)
        {
            char[] chars = s.ToCharArray();
            foreach (char c in chars)
            {
                if (c.IsEmpty()) return true;
            }

            return false;
        }

        /// <summary>
        /// Adds n padding character to the end of the string.
        /// </summary>
        /// <param name="instr">The string to be padded.</param>
        /// <param name="c">The character to pad with.</param>
        /// <param name="len">The number of padding character to apply.</param>
        /// <returns>A string contianing the original string followed by a set number of padding 
        /// characters.</returns>
        /// Contributed by Russell Dehart
        public static string PostPadChar(this string instr, char c, int len)
        {
            StringBuilder results = new StringBuilder(instr);
            try
            {
                for (int i = 0; i < len; i++)
                {
                    results.Append(c);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException("Enlarging the value of this instnace would exceed max capacity", ex);
            }
            catch
            {
                throw;
            }

            return results.ToString();
        }

        /// <summary>
        ///  Adds padding characters to the end of the string to reach a set length.
        /// </summary>
        /// <param name="instr">The string to be padded.</param>
        /// <param name="c">The character to pad with.</param>
        /// <param name="len">The length of the string to be padded to.</param>
        /// <returns>A string contianing the original string follwed by padding characters to 
        /// reach the specified length.</returns>
        /// Contributed by Russell Dehart
        public static String PostPadToLength(this string instr, char c, int len)
        {
            int paddLen = len - instr.Length;
            if (paddLen < 0)
            {
                throw new ArgumentException("The string passed in is longer than the length specified");
            }

            if (paddLen > 0)
            {
                return PostPadChar(instr, c, paddLen);
            }
            else
            {
                return instr;
            }
        }

        /// <summary>
        /// Adds n padding characters to the beginning of the string.
        /// </summary>
        /// <param name="instr">The string to be padded.</param>
        /// <param name="c">The character to pad with.</param>
        /// <param name="len">The number of padding character to apply.</param>
        /// <returns>A string contianing the original string preceeded by a set number of padding 
        /// characters.</returns>
        /// Contributed by Russell Dehart
        public static string PrePadChar(this string instr, char c, int len)
        {
            StringBuilder results = new StringBuilder();
            try
            {
                for (int i = 0; i < len; i++)
                {
                    results.Append(c);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException("Enlarging the value of this instnace would exceed max capacity", ex);
            }
            catch
            {
                throw;
            }

            results.Append(instr);

            return results.ToString();
        }

        /// <summary>
        /// Adds padding characters to the beginning of the string to reach a set length.
        /// </summary>
        /// <param name="instr">The string to be padded.</param>
        /// <param name="c">The character to pad with.</param>
        /// <param name="len">The length of the string to be padded to.</param>
        /// <returns>A string contianing the original string preceeded by padding characters to 
        /// reach the specified length.</returns>
        /// Contributed by Russell Dehart
        public static String PrePadToLength(this string instr, char c, int len)
        {
            int paddLen = len - instr.Length;
            if (paddLen < 0)
            {
                throw new ArgumentException("The string passed in is longer than the length specified");
            }

            if (paddLen > 0)
            {
                return PrePadChar(instr, c, paddLen);
            }
            else
            {
                return instr;
            }
        }

        /// <summary>
        /// Removes the last n characters off the string.
        /// </summary>
        /// <param name="instr">The string being shortened.</param>
        /// <param name="length">The number of characters to be removed from the end of the 
        /// string.</param>
        /// <returns>A new string contianing the contents of the original string less the last n 
        /// characters.</returns>
        /// Contributed by Russell Dehart
        public static string RemoveLast(this String instr, int length)
        {
            try
            {
                return instr.Substring(0, instr.Length - length);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentException("The starting index plus the length indicates a position not within this instance.", ex);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Removes the last character off the string.
        /// </summary>
        /// <param name="instr">The string being shortened.</param>
        /// <returns>A new string contianing the contents of the original string less the last 
        /// character.</returns>
        /// Contributed by Russell Dehart
        public static string RemoveLastChar(this String instr)
        {
            return RemoveLast(instr, 1);
        }

        /// <summary>
        /// Retrieves the first n characters froma string.
        /// </summary>
        /// <param name="instr">The string being shortened.</param>
        /// <param name="startIndex">The number of characters to be retrieved from the beginning 
        /// of the string.</param>
        /// <returns>A new string contianing the first n characters of the original string.</returns>
        /// Contributed by Russell Dehart
        public static string RetrieveFirst(this String instr, int startIndex)
        {
            try
            {
                return instr.Substring(0, startIndex);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentException("startIndex is less than zero or greater than the length of the instance.", ex);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves the first character from a string.
        /// </summary>
        /// <param name="instr">The string being shortened.</param>
        /// <returns>A new string contianing the first character of the original string.</returns>
        /// Contributed by Russell Dehart
        public static char RetrieveFirstChar(this String instr)
        {
            return RetrieveFirst(instr, 1).ToCharArray()[0];
        }

        /// <summary>
        /// Retrieves the last n characters froma string.
        /// </summary>
        /// <param name="instr">The string being shortened.</param>
        /// <param name="startIndex">The number of characters to be retrieved from the end of the 
        /// string.</param>
        /// <returns>A new string contianing the last n characters of the original string.</returns>
        /// Contributed by Russell Dehart
        public static string RetrieveLast(this String instr, int n)
        {
            try
            {
                return instr.Substring(instr.Length - n, n);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentException("n is less than zero or greater than the length of the instance.", ex);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Retrieves the last character from a string.
        /// </summary>
        /// <param name="instr">The string being shortened.</param>
        /// <returns>A new string contianing the last character of the original string.</returns>
        /// Contributed by Russell Dehart
        public static char RetrieveLastChar(this String instr)
        {
            return RetrieveFirst(instr, 1).ToCharArray()[0];
        }

        /// <summary>
        /// Convert a string into a Stream object.
        /// </summary>
        /// <param name="instr">The string to be converted.</param>
        /// <returns>A Stream containing the contents of the string.</returns>
        /// Contributed by Russell Dehart
        public static Stream ToStream(this string instr)
        {
            byte[] byteArray;

            try
            {
                byteArray = Encoding.UTF8.GetBytes(instr);
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("Value passed in is null", ex);
            }
            catch (EncoderFallbackException)
            {
                throw;
            }

            try
            {
                return new MemoryStream(byteArray);
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("Buffer is null", ex);
            }
        }

        /// <summary>
        /// Returns the length of the trimmmed string.
        /// </summary>
        /// <param name="instr">The string to be evaluated.</param>
        /// <returns>The number of characters in the trimmed string.</returns>
        /// Contributed by Russell Dehart
        public static int TrimLength(this string instr)
        {
            return instr.Trim().Length;
        }

        /// <summary>
        /// Searches the input string for the first occurnace of the specified regular expression, 
        /// using the specified matching options.
        /// </summary>
        /// <param name="instr">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="options">A bitwise combination of the enumeration values that provide 
        /// options for matching.</param>
        /// <param name="groupName">The name of the group matched by the regular expresson.</param>
        /// <returns>An object that contains information about the match.</returns>
        /// Contributed by Russell Dehart
        public static string RegexMatch(this string instr, string pattern, RegexOptions options, string groupName)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(groupName))
                    return Regex.Match(instr, pattern, options).Value;
                else
                    return Regex.Match(instr, pattern, options).Groups[groupName].Value;
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("The input or the pattern is null.", ex);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException("Options parameter is not a valid bitwise combination of RegexOption values or matchTimeout is out of range.", ex);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("A regular expresssion parsing error occured.", ex);
            }
            catch (RegexMatchTimeoutException ex)
            {
                throw new RegexMatchTimeoutException("A time-out occured.", ex);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Searches the input string for the first occurnace of the specified regular expression.
        /// </summary>
        /// <param name="instr">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <returns>An object that contains information about the match.</returns>
        /// Contributed by Russell Dehart
        public static string RegexMatch(this string instr, string pattern)
        {
            try
            {
                return RegexMatch(instr, pattern, RegexOptions.None, null);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Searches the input string for the first occurnace of the specified regular expression, 
        /// using the specified matching options.
        /// </summary>
        /// <param name="instr">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="options">A bitwise combination of the enumeration values that provide 
        /// options for matching.</param>
        /// <returns>An object that contains information about the match.</returns>
        /// Contributed by Russell Dehart
        public static string RegexMatch(this string instr, string pattern, RegexOptions options)
        {
            try
            {
                return RegexMatch(instr, pattern, options, null);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Searches the input string for the first occurnace of the specified regular expression.
        /// </summary>
        /// <param name="instr">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="groupName">The name of the group matched by the regular expresson.</param>
        /// <returns>An object that contains information about the match.</returns>
        /// Contributed by Russell Dehart
        public static string RegexMatch(this string instr, string pattern, string groupName)
        {
            try
            {
                return RegexMatch(instr, pattern, RegexOptions.None, groupName);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// In a specified input string, replaces all strings that match a specified regular 
        /// expression with a specified replacement string. Specified options modify the 
        /// matching operation.
        /// </summary>
        /// <param name="instr">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="replacement">The replacement string.</param>
        /// <param name="options">A bitwise combination of the enumeration values that provide 
        /// options for matching.</param>
        /// <returns>A new string that is identical to the input string, except that the 
        /// replacement string takes the place of each matched string. If pattern is not matched 
        /// in the current instance, the method returns the current instance unchanged.</returns>
        /// Contributed by Russell Dehart
        public static string RegexReplace(this string instr, string pattern, string replacement, RegexOptions options)
        {
            try
            {
                return Regex.Replace(instr, pattern, replacement, options);
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("The input, pattern or replacement is null.", ex);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException("Options parameter is not a valid bitwise combination of RegexOption values.", ex);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("A regular expresssion parsing error occured.", ex);
            }
            catch (RegexMatchTimeoutException ex)
            {
                throw new RegexMatchTimeoutException("A time-out occured.", ex);
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// In a specified input string, replaces all strings that match a specified regular 
        /// expression with a specified replacement string. 
        /// </summary>
        /// <param name="instr">The string to search for a match.</param>
        /// <param name="pattern">The regular expression pattern to match.</param>
        /// <param name="replacement">The replacement string.</param>
        /// <returns>A new string that is identical to the input string, except that the 
        /// replacement string takes the place of each matched string. If pattern is not matched 
        /// in the current instance, the method returns the current instance unchanged.</returns>
        /// Contributed by Russell Dehart
        public static string RegexReplace(this string instr, string pattern, string replacement)
        {
            try
            {
                return Regex.Replace(instr, pattern, replacement, RegexOptions.None);
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("The input, pattern or replacement is null.", ex);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException("Options parameter is not a valid bitwise combination of RegexOption values.", ex);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("A regular expresssion parsing error occured.", ex);
            }
            catch (RegexMatchTimeoutException ex)
            {
                throw new RegexMatchTimeoutException("A time-out occured.", ex);
            }
            catch
            {
                throw;
            }
        }
    }
}
