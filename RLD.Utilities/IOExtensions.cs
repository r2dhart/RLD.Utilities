using System;
using System.IO;

namespace RLD.Utilities.Extensions
{
    /// <summary>
    /// IOExtensions Class
    /// 
    /// Contains the extension methods I have developed to add onto the System.IO library.
    /// </summary>
    public static class IOExtensions
    {
        private const int OneKByteSize = 1024;
        private const string FileSizeFormat = "F0";
        private const int EightKBufferSize = 8092;

        /// <summary>
        /// Converts file size to the correct size metric.
        /// </summary>
        /// <param name="size">A long containing the number of bytes to be converted.</param>
        /// <returns>A string contaiing the formatted file size with the corresponding metric.</returns>
        /// Contributed by Russell Dehart
        public static string ToFileSize(this long size)
        {
            try
            {
                if (size < OneKByteSize)
                    return (size).ToString(FileSizeFormat) + " bytes";
                if (size < Math.Pow(OneKByteSize, 2))
                    return (size / OneKByteSize).ToString(FileSizeFormat) + "KB";
                if (size < Math.Pow(OneKByteSize, 3))
                    return (size / Math.Pow(OneKByteSize, 2)).ToString(FileSizeFormat) + "MB";
                if (size < Math.Pow(OneKByteSize, 4))
                    return (size / Math.Pow(OneKByteSize, 3)).ToString(FileSizeFormat) + "GB";
                if (size < Math.Pow(OneKByteSize, 5))
                    return (size / Math.Pow(OneKByteSize, 4)).ToString(FileSizeFormat) + "TB";
                if (size < Math.Pow(OneKByteSize, 6))
                    return (size / Math.Pow(OneKByteSize, 5)).ToString(FileSizeFormat) + "PB";
                return (size / Math.Pow(OneKByteSize, 6)).ToString(FileSizeFormat) + "EB";
            }
            catch (FormatException)
            {
                throw;
            }

        }

        /// <summary>
        /// Copies an XmlDocument to a Stream object.
        /// </summary>
        /// <param name="doc">The XMLDocument to be copied to a Stram object.</param>
        /// <returns>A Stream object containin the contents of the XmlDocument.</returns>
        /// Contributed by Russell Dehart
        public static Stream ToMemoryStream(this System.Xml.XmlDocument doc)
        {
            MemoryStream results = new MemoryStream();

            try
            {
                // Save the XMl doc to the stream
                doc.Save(results);

                results.Flush();
                // Move to the begining of the stream
                results.Position = 0;
            }
            catch (System.Xml.XmlException)
            {
                throw;
            }
            catch (ArgumentOutOfRangeException)
            {
                throw;
            }
            catch (ObjectDisposedException ex)
            {
                throw new ObjectDisposedException("The Memory Stream object has been closed.", ex);
            }
            catch (IOException)
            {
                throw;
            }


            return results;
        }

        /// <summary>
        /// Converts to contents of a stream object to a string.
        /// </summary>
        /// <param name="streamin">A stream object to have it's contents converted to a string.</param>
        /// <returns>A string containing the contents of the Stream object.</returns>
        /// Contributed by Russell Dehart
        public static string ToString(this Stream streamin)
        {
            StreamReader reader = new StreamReader(streamin);

            try
            {
                return reader.ReadToEnd();
            }
            catch (OutOfMemoryException ex)
            {
                throw new ArgumentException("There is insufficient memory to allocate a buffer for the returned string.", ex);
            }
            catch (IOException ex)
            {
                throw new ArgumentException("An I/O error occurs.", ex);
            }
            catch
            {
                throw;
            }

        }

        /// <summary>
        /// Copies aone Stream object to another.
        /// </summary>
        /// <param name="fromStream">The stream to copy from.</param>
        /// <param name="toStream">The stream to copy to.</param>
        /// Contributed by Russell Dehart
        public static void CopyTo(this Stream fromStream, Stream toStream)
        {
            // Check for null parameters
            if (fromStream == null) throw new ArgumentNullException("fromStream");
            if (toStream == null) throw new ArgumentNullException("toStream");

            // Copy the contents from one buffer to another
            byte[] buffer = new byte[EightKBufferSize];
            int dataRead;
            try
            {
                while ((dataRead = fromStream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    toStream.Write(buffer, 0, dataRead);
                }
            }
            catch (ArgumentNullException ex)
            {
                throw new ArgumentException("buffer is null.", ex);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                throw new ArgumentException("offset or count is negative.", ex);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException("The sum of offset and count is greater than the buffer length.", ex);
            }
            catch (IOException ex)
            {
                throw new ArgumentException("An I/O error occured.", ex);
            }
            catch (NotSupportedException ex)
            {
                throw new ArgumentException("The stream does not support reading and/or writing.", ex);
            }
            catch (ObjectDisposedException ex)
            {
                throw new ArgumentException("Methods were called after the stream was closed.", ex);
            }
            catch
            {
                throw;
            }
        }
    }
}
