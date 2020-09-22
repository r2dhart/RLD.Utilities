using System;
using System.Collections.Generic;
using System.Linq;

namespace RLD.Utilities.Extensions
{
    /// <summary>
    /// GenericExtensions Class
    /// 
    /// Contains the extension methods I have developed to add onto the System.Collections.Generic library.
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        /// Adds a value to the caller.
        /// </summary>
        /// <typeparam name="T">The generic value type.</typeparam>
        /// <param name="type">The enum to perform this function on.</param>
        /// <param name="value">The value to be checked for.</param>
        /// <returns>An object of the calling type with the value applied to it.</returns>
        /// Contributed by Russell Dehart
        public static T Add<T>(this Enum type, T value)
        {
            try
            {
                int t = (int)(object)type;
                int v = (int)(object)value;

                return (T)(Object)(t | v);
            }
            catch (Exception ex)
            {
                String typeName = typeof(T).Name;
                throw new ArgumentException($"Could not append value from enumerated type'{typeName}'", ex);
            }
        }

        /// <summary>
        /// Gets the value associated with a specified key. Includes nulls.
        /// </summary>
        /// <typeparam name="TKey">The key values of the dictionaty object.</typeparam>
        /// <typeparam name="TValue">The value associated to the key.</typeparam>
        /// <param name="dictionary">The dictionary to be searched for the key.</param>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key or the default value if not key is 
        /// found.</returns>
        /// Contributed by Russell Dehart
        public static Nullable<TValue> GetValueOrNull<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
            where TValue : struct
        {
            try
            {
                // If the dictionary contains the value return it.
                if (dictionary.TryGetValue(key, out TValue result)) return result;

                // Otherwise return null
                return null;
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("Key is Null.", ex);
            }
        }

        /// <summary>
        /// Determines if the value specified is present in the Enum.
        /// </summary>
        /// <typeparam name="T">The generic value type.</typeparam>
        /// <param name="type">The enum to perform this function on.</param>
        /// <param name="value">The value to be checked for.</param>
        /// <returns>A boolean indicating if the value is present in the Enum.</returns>
        /// Contributed by Russell Dehart
        public static bool Has<T>(this Enum type, T value)
        {
            bool results = false;

            try
            {
                int t = (int)(object)type;
                int v = (int)(object)value;
                results = ((t & v) == v);
            }
            catch
            {
                results = false;
            }

            return results;
        }

        /// <summary>
        /// Determines if the value specified is the Enum.
        /// </summary>
        /// <typeparam name="T">The generic value type.</typeparam>
        /// <param name="type">The enum to perform this function on.</param>
        /// <param name="value">The value to be checked for.</param>
        /// <returns>A boolean indicating if the value is present in the Enum.</returns>
        /// Contributed by Russell Dehart
        public static bool Is<T>(this Enum type, T value)
        {
            bool results = false;
            try
            {
                int t = (int)(object)type;
                int v = (int)(object)value;

                results = (t == v);
            }
            catch
            {
                results = false;
            }

            return results;
        }

        /// <summary>
        /// Determines if a value is present in a list of values.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The value to be searched for.</param>
        /// <param name="list">The values to be searched.</param>
        /// <returns>A boolean indicating if the source is present in the list.</returns>
        /// Contributed by Russell Dehart
        public static bool IsIn<T>(this T source, params T[] list)
        {
            if (null == source) throw new ArgumentNullException("source");

            try
            {
                return list.Contains(source);
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("Source is null", ex);
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// Determines if the value is the first to occur in the provided list.
        /// </summary>
        /// <typeparam name="T">The list type</typeparam>
        /// <param name="value">The value to  be compared.</param>
        /// <param name="list">The list to be compared.</param>
        /// <returns>Returns a boolean indicating if the value is the first occurence in the list.</returns>
        /// Contributed by Russell Dehart
        public static Boolean IsFirst<T>(this T value, IEnumerable<T> list)
        {
            bool results;

            try
            {
                results = (Comparer<T>.Default.Compare(value, list.First()) >= 0);
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("Source is null.", ex);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("Type T does not implemntt either IComparable<T> generic interface or the IComparable interface.", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new InvalidOperationException("The source sequence is empty.", ex);
            }
            return results;
        }

        /// <summary>
        /// Emulates the PYHTON Join method.
        /// </summary>
        /// <typeparam name="T">The list type</typeparam>
        /// <param name="separator">The string to be used as a separator.</param>
        /// <param name="list">The list of items to be converted to a string.</param>
        /// <returns>A Concatenated list of items using the specified separator.</returns>
        /// Contributed by Russell Dehart
        public static string Join<T>(this string separator, IEnumerable<T> list)
        {
            try
            {
                return String.Join(separator, list.ToArray());
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("The list is null.", ex);
            }

        }

        /// <summary>
        /// Emulates the PYHTON Join method.
        /// </summary>
        /// <typeparam name="T">The list type</typeparam>
        /// <param name="separator">The character to be used as a separator.</param>
        /// <param name="list">The list of items to be converted to a string.</param>
        /// <returns>A Concatenated list of items using the specified separator.</returns>
        /// Contributed by Russell Dehart
        public static string Join<T>(this char separator, IEnumerable<T> list)
        {
            return Join(separator.ToString(), list);
        }

        /// <summary>
        /// Retrieves a random member of an IEnumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static T Random<T>(this List<T> obj)
        {
            Random _rnd = new Random();
            var randomIndex = _rnd.Next(0, obj.Count);
            return obj[randomIndex];
        }

        /// <summary>
        /// Removes a value from the caller.
        /// </summary>
        // <typeparam name="T">The generic value type.</typeparam>
        /// <param name="type">The enum to perform this function on.</param>
        /// <param name="value">The value to be checked for.</param>
        /// <returns>An object of the calling type with the value remove from it.</returns>
        /// Contributed by Russell Dehart
        public static T Remove<T>(this System.Enum type, ThreadStaticAttribute value)
        {
            try
            {
                int t = (int)(object)type;
                int v = (int)(object)value;

                return (T)(Object)(t & ~v);
            }
            catch (Exception ex)
            {
                String typeName = typeof(T).Name;
                throw new ArgumentException($"Could not remove value from enumerated type'{typeName}'", ex);
            }
        }

        /// <summary>
        /// Converts one data type to another.
        /// </summary>
        /// <typeparam name="T">The data type to be converted to.</typeparam>
        /// <param name="obj">The value to be converted.</param>
        /// <returns>A value of the type specified in the most fiting format given the values original data type.</returns>
        /// Contributed by Russell Dehart
        public static T To<T>(this IConvertible obj)
        {
            try
            {
                return (T)Convert.ChangeType(obj, typeof(T));
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidCastException("This conversion is not supported or value does not implement the IConvertible interface.", ex);
            }
            catch (FormatException ex)
            {
                throw new FormatException("Value is not in a format recognized by the conversion type.", ex);
            }
            catch (OverflowException ex)
            {
                throw new OverflowException("Value represents a number that is out of range of the conversion type.", ex);
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentNullException("Conversion type is null", ex);
            }
            catch
            {
                throw;
            }
        }
    }
}
