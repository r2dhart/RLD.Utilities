using System;

namespace RLD.Utilities.Extensions
{
    /// <summary>
    /// CharExtensions Class
    /// 
    /// Contains the extension methods I have developed to add onto the System.char library.
    /// </summary>
    public static class CharExtensions
    {
        /// <summary>
        /// Applys a 'shift' to the character resulting in the keyboard equivalent of pressing the 
        /// key and shift.
        ///  - Lower case are converted to uppercase.
        ///  - Upper case are converted to lower case.
        ///  - etc.
        /// </summary>
        /// <param name="c">The character to be 'shifted'.</param>
        /// <returns>A shifted version of the character supplied.</returns>
        public static char ApplyShift(this char c)
        {
            char results = c;

            if (c.IsAlphaLower())
            {
                results = c.ConvertToAlphaUpper();
            }
            else if (c.IsAlphaUpper())
            {
                results = c.ConvertToAlphaLower();
            }
            else
            {
                switch (c)
                {
                    case '0':
                        results = ')';
                        break;
                    case '1':
                        results = '!';
                        break;
                    case '2':
                        results = '@';
                        break;
                    case '3':
                        results = '#';
                        break;
                    case '4':
                        results = '$';
                        break;
                    case '5':
                        results = '%';
                        break;
                    case '6':
                        results = '^';
                        break;
                    case '7':
                        results = '&';
                        break;
                    case '8':
                        results = '*';
                        break;
                    case '9':
                        results = '(';
                        break;
                    case '-':
                        results = '_';
                        break;
                    case '=':
                        results = '+';
                        break;
                    case '[':
                        results = '{';
                        break;
                    case ']':
                        results = '}';
                        break;
                    case '\\':
                        results = '|';
                        break;
                    case ';':
                        results = ':';
                        break;
                    case '\'':
                        results = '"';
                        break;
                    case ',':
                        results = '<';
                        break;
                    case '.':
                        results = '>';
                        break;
                    case '/':
                        results = '?';
                        break;
                    case '_':
                        results = '-';
                        break;
                    case '+':
                        results = '=';
                        break;
                    case '{':
                        results = '[';
                        break;
                    case '}':
                        results = ']';
                        break;
                    case '|':
                        results = '\\';
                        break;
                    case ':':
                        results = ';';
                        break;
                    case '"':
                        results = '\'';
                        break;
                    case '<':
                        results = ',';
                        break;
                    case '>':
                        results = '.';
                        break;
                    case '?':
                        results = '/';
                        break;
                    default:
                        throw new ArgumentException("invalid  character type");
                }
            }

            return results;
        }

        /// <summary>
        /// Converts a character to it's lowercase equivalent.
        /// </summary>
        /// <param name="c">The character to be evaluated.</param>
        /// <returns>A lower case character.</returns>
        /// Contributed by Russell Dehart
        public static char ConvertToAlphaLower(this char c)
        {
            string s = c.ToString();
            char[] results = s.ToLower().ToCharArray();
            return results[0];
        }

        /// <summary>
        /// Converts a character to it's upercase equivalent.
        /// </summary>
        /// <param name="c">The character to be evaluated.</param>
        /// <returns>An upper case character.</returns>
        /// Contributed by Russell Dehart
        public static char ConvertToAlphaUpper(this char c)
        {
            string s = c.ToString();
            char[] results = s.ToUpper().ToCharArray();
            return results[0];
        }

        /// <summary>
        /// Determines if the character is alpha (case insenitive)
        /// </summary>
        /// <param name="c">The character to be evaluated.</param>
        /// <returns>True if alpha, false otherwise.</returns>
        /// Contributed by Russell Dehart
        public static bool IsAlpha(this char c)
        {
            return IsAlphaUpper(c) || IsAlphaLower(c);
        }

        /// <summary>
        /// Determines if the character is a lower case alpha character.
        /// </summary>
        /// <param name="c">The character to be evaluated.</param>
        /// <returns>True if lower case alpha, false otherwise.</returns>
        /// Contributed by Russell Dehart
        public static bool IsAlphaLower(this char c)
        {
            return (c >= 'a' && c <= 'z');
        }

        /// <summary>
        /// Determines if the character is a upper case alpha character.
        /// </summary>
        /// <param name="c">The character to be evaluated.</param>
        /// <returns>True if upper case alpha, false otherwise.</returns>
        /// Contributed by Russell Dehart
        public static bool IsAlphaUpper(this char c)
        {
            return (c >= 'A' && c <= 'Z');
        }

        /// <summary>
        /// Determines if the character is blank space
        /// </summary>
        /// <param name="c">The character to be evaluated.</param>
        /// <returns>True if empty, false otherwise.</returns>
        /// Contributed by Russell Dehart
        public static bool IsEmpty(this char c)
        {
            return c == ' ';
        }

        /// <summary>
        /// Determines if the character is a numeric character.
        /// </summary>
        /// <param name="c">The character to be evaluated.</param>
        /// <returns>True if numeric, false otherwise.</returns>
        /// Contributed by Russell Dehart
        public static bool IsNumeric(this char c)
        {
            return "0123456789".Contains(c.ToString());
        }

        /// <summary>
        /// Switches the case of an alpha character.
        /// </summary>
        /// <param name="c">The character to be evaluated.</param>
        /// <returns>Swaps the case of the character provided. If not alpha it passes back the value passed in.</returns>
        /// Contributed by Russell Dehart
        public static char ReverseCase(this char c)
        {
            if (c.IsAlphaLower()) return c.ConvertToAlphaUpper();
            else if (c.IsAlphaUpper()) return c.ConvertToAlphaLower();
            else return c;

        }
    }
}
