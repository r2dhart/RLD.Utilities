using System;
using System.Text;

namespace RLD.Utilities.Extensions
{
    /// <summary>
    /// Exception extension class
    /// 
    /// Contains the extension methods I have developed to add onto the System.Exception library.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Formats an exception for being written to a log.
        /// </summary>
        /// <param name="e">The exception</param>
        /// <returns>Returns a string containing the formatted exception message.</returns>
        public static string ToLoggingString(this Exception e)
        {
            StringBuilder results = new StringBuilder();

            results.Append($"********** {DateTime.Now} **********");
            if (e.InnerException != null)
            {
                results.AppendLine($"Inner Exception Type: {e.InnerException.GetType().ToString()}");
                results.AppendLine($"Inner Exception: {e.InnerException.Message}");
                results.AppendLine($"Inner Source: {e.InnerException.Source}");
                if (e.InnerException.StackTrace != null)
                {
                    results.AppendLine($"Inner Stack Trace: {e.InnerException.StackTrace}");
                }

                results.AppendLine($"Exception Type: {e.GetType().ToString()}");
                results.AppendLine($"Exception: {e.Message}");
                results.AppendLine($"Source: { e.Source}");
                results.AppendLine("Stack Trace: ");
                if (e.StackTrace != null)
                {
                    results.AppendLine(e.StackTrace);
                    results.AppendLine();
                }
            }

            return results.ToString();

        }
    }
}
