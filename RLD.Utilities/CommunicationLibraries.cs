using System;
using System.ComponentModel;
using System.Net.Mail;
using System.Text.RegularExpressions;

/// <summary>
/// Namespace for common libraries
/// </summary>
namespace RLD.Utilities.Libraries
{
    /// <summary>
    /// A class for exposing communications methods.
    /// </summary>
    public static class CommunicationUtils
    {
        // TODO: Move this section into a config file
        private const string DefaultSmtpHost = "172.20.1.232";
        private const int DefaultTimeoutInterval = 5000;
        private const short DefaultPort = 25;
        private const string DefaultDomainPassword = "D!m]jc8<8#a8:WFQ";
        private const string DefaultDomainName = "hendrickson-intl";

        /// <summary>
        /// Basic SMTP sendmail function
        /// </summary>
        /// <param name="recipientAddress">The email address of the recipient.</param>
        /// <param name="subject">The subject to be disaplyed for the email.</param>
        /// <param name="messageBody">The text of the email.</param>
        /// <param name="hostName">The optional name of the host.</param>
        /// <param name="domain">An optional name for the domain. If this is supplied then a password most be supplied for the domain as well.</param>
        /// <param name="password">An optional parameter to supply the password for the optional domain.</param>
        /// <param name="port">An optional port (Defults to 25).</param>
        /// <param name="sendAsHtml">An optional parameter to indicate if the message should be sent as HTML (Defualt is false).</param>
        /// Contributed by Russell Dehart
        public static void SendSmtpEmail(string recipientAddress, string subject, string messageBody, string senderAddress, string hostName = DefaultSmtpHost, string domain = DefaultDomainName, string password = DefaultDomainPassword, short port = DefaultPort, bool sendAsHtml = false, int timeout = DefaultTimeoutInterval)
        {
            // use the Host object to validate the hostname
            Host host = new Host(hostName);
            SmtpClient client = new SmtpClient(host.Address, port);

            MailMessage message = new MailMessage();

            // validate the recipientAddress
            try
            {
                MailAddress recipient = new MailAddress(recipientAddress);
                MailAddress sender = new MailAddress(senderAddress);

                // Create the message object                
                message.Body = messageBody;
                message.BodyEncoding = System.Text.Encoding.UTF8;
                message.Subject = subject;
                message.SubjectEncoding = System.Text.Encoding.UTF8;
                message.To.Add(recipient);
                message.From = sender;
                message.IsBodyHtml = sendAsHtml;

                // Create the client object
                client.Timeout = timeout;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.EnableSsl = false;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(sender.Address, password, domain);
                client.SendCompleted += new SendCompletedEventHandler(sendCompletedCallback);

                client.Send(message);
            }
            catch (ArgumentNullException ex)
            {
                string msg = (String.IsNullOrWhiteSpace(recipientAddress)) ? "recipientAddress parameter is null." : "senderAddress parameter is empty.";
                throw new ArgumentNullException(msg, ex);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                string msg = "Timeout less than zero (0).";
                throw new ArgumentOutOfRangeException(msg, ex);
            }
            catch (ArgumentException ex)
            {
                string msg = (String.IsNullOrWhiteSpace(recipientAddress)) ? "recipientAddress parameter is empty." : "senderAddress parameter is empty.";
                throw new ArgumentException(msg, ex);
            }
            catch (FormatException ex)
            {
                string msg = (String.IsNullOrWhiteSpace(recipientAddress)) ? "recipientAddress parameter is not in a recognized format." : "senderAddress parameter is not in a recognized format.";
                throw new FormatException(msg, ex);
            }
            catch (InvalidOperationException ex)
            {
                string msg = "The timeout, Credentials and UseDefaultCredentials properties cannot be changed while an email is being sent.";
                throw new ArgumentException(msg, ex);
            }
            finally
            {
                // Housekeeping
                message.Dispose();
            }
        }

        private static void sendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            // Get the token for the asynchronous task
            string token = (string)e.UserState;

            // Log the results
            if (e.Cancelled)
            {
                throw new OperationCanceledException();
            }
            else if (e.Error != null)
            {
                throw new Exception(e.Error.Message);
            }
        }

        public class Host
        {
            // private members
            // Used to validate the address as an IP address
            Regex ipRegex = new Regex(@"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$");
            // Used to validate the address as a hostname
            Regex hostnameRegex = new Regex(@"^(([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z0-9]|[A-Za-z0-9][A-Za-z0-9\-]*[A-Za-z0-9])$");

            // Public members
            /// <summary>
            /// Exposes the host name as string
            /// </summary>
            public string Address { get; protected set; }

            // Private methods

            // Public mehtods

            // Constructors
            public Host(string address)
            {
                // Perform basic validation of address
                if (String.IsNullOrWhiteSpace(address)) throw new ArgumentNullException("Null or empty host");

                // Validate the host name
                if (ipRegex.IsMatch(address) || hostnameRegex.IsMatch(address))
                {
                    Address = address;
                }
                else
                {
                    throw new ArgumentException("Invlaid host name.");
                }
            }
        }
    }
}
