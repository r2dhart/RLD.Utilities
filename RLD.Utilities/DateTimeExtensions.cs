using System;

namespace RLD.Utilities.Extensions
{
    /// <summary>
    /// DateTimeExtensions Class
    /// 
    /// Contains the extension methods I have developed to add onto the System.DateTime library.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Checks to see if a date falls between two dates.
        /// </summary>
        /// <param name="dt">The date to be evaluated.</param>
        /// <param name="rangeBeg">The beginning date for comparrison.</param>
        /// <param name="rangeEnd">The ending date for comparrison.</param>
        /// <returns>A boolean indicating if the date is beteween the dates provided.</returns>
        /// Contributed by Russell Dehart
        public static bool IsBetween(this DateTime dt, DateTime rangeBeg, DateTime rangeEnd)
        {
            return dt.Ticks >= rangeBeg.Ticks && dt.Ticks <= rangeEnd.Ticks;
        }

        /// <summary>
        /// Determines the age of something.
        /// </summary>
        /// <param name="dtin">The date of the object to calculate the age of.</param>
        /// <returns>An integer indicating the age of the object in years.</returns>
        /// Contributed by Russell Dehart
        public static int CaluclateAge(this DateTime dtin)
        {
            var results = DateTime.Now.Year - dtin.Year;
            try
            {
                if (DateTime.Now < dtin.AddYears(results))
                    results--;
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("Value or the resulting DateTime is less than MinValue or greater than MaxValue", ex);
            }
            catch
            {
                throw;
            }

            return results;
        }

        /// <summary>
        /// Determines if the date is a workday.
        /// </summary>
        /// <param name="dtin">The date to be evaluated.</param>
        /// <returns>A boolean indicating if the date falls on a weekday.</returns>
        /// Contributed by Russell Dehart
        public static bool IsWorkingDay(this DateTime dtin)
        {
            return dtin.DayOfWeek != DayOfWeek.Saturday && dtin.DayOfWeek != DayOfWeek.Sunday;
        }

        /// <summary>
        /// Determines if the date is a weekend.
        /// </summary>
        /// <param name="dtin">The date to be evaluated.</param>
        /// <returns>>A boolean indicating if the date falls on a weekend.</returns>
        /// Contributed by Russell Dehart
        public static bool IsWeekendDay(this DateTime dtin)
        {
            return dtin.DayOfWeek == DayOfWeek.Saturday || dtin.DayOfWeek == DayOfWeek.Sunday;
        }

        /// <summary>
        /// Returns the next non-weekend day.
        /// </summary>
        /// <param name="dtin">The date to be evaluated.</param>
        /// <returns>A dateTime contaiing the next non-weekend day.</returns>
        /// Contributed by Russell Dehart
        public static DateTime GetNextWorkDay(this DateTime dtin)
        {
            DateTime results = dtin;

            try
            {

                if (results.IsWorkingDay())
                {
                    results = results.AddDays(1);
                }

                while (results.IsWeekendDay())
                {
                    results = results.AddDays(1);
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException("The resulting DateTime is less than MinValue or greater than MaxValue.", ex);
            }
            catch
            {
                throw;
            }

            return results;
        }

        /// <summary>
        /// Determine the next date by passing in the Day of the week.
        /// </summary>
        /// <param name="dtin">The date to be evaluated.</param>
        /// <param name="dayOfWeek">The next day of the week to look for.</param>
        /// <returns>The date for the next day oF the week indicated.</returns>
        /// Contributed by Russell Dehart
        public static DateTime Next(this DateTime dtin, DayOfWeek dayOfWeek)
        {
            // Calculate offset days
            int offsetDays = dayOfWeek - dtin.DayOfWeek;
            if (offsetDays <= 0)
            {
                offsetDays += 7;
            }

            DateTime results;

            try
            {
                results = dtin.AddDays(offsetDays);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentOutOfRangeException("The resulting DateTime is less than MinValue or greater than MaxValue.", ex);
            }
            catch
            {
                throw;
            }

            return results;
        }
    }
}
