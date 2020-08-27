using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Dayoff.BLL.Services
{
    public class EmailService
    {
        public string SMTPServer { get; private set; }
        public int PortNumber { get; private set; }
        private MailMessage Message { get; set; }

        private readonly Regex _regex = new Regex("\\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,4}\\b",
                RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.Compiled);

        public EmailService(string smtpServer, int portNumber)
        {
            SMTPServer = smtpServer;
            PortNumber = portNumber;
        }

        public void CreateMessage(string toAddressTitle, string toAddress, string fromAddressTitle, string fromAddress, string subject)
        {
            if (string.IsNullOrEmpty(toAddress))
            {
                throw new ArgumentNullException("to", "Unable to create Message: empty to address");
            }

            if (string.IsNullOrEmpty(fromAddress))
            {
                throw new ArgumentNullException("from", "Unable to create Message: empty from address");
            }

            if (!_regex.IsMatch(toAddress))
            {
                throw new ArgumentException("Unable to create Message: invalid to address format", "to");
            }

            if (!_regex.IsMatch(fromAddress))
            {
                throw new ArgumentException("Unable to create Message: invalid from address format", "from");
            }

            var from = new MailAddress(fromAddress, fromAddressTitle);
            var to = new MailAddress(toAddress, toAddressTitle);

            Message = new MailMessage(from, to);
            Message.IsBodyHtml = true;
            Message.Subject = subject;
        }

        public void LoadTemplate(string template)
        {
            if (string.IsNullOrEmpty(template) || !File.Exists(template))
            {
                throw new ArgumentNullException("template", "a valid file path to an email template is required");
            }

            using (StreamReader reader = new StreamReader(template))
            {
                Message.Body = reader.ReadToEnd();
            }
        }

        public void ReplaceTemplateProperty(string property, string value)
        {
            Message.Body = Message.Body.Replace(property, value);
        }

        public void SetMessageBody(string body)
        {
            Message.Body = body;
        }

        public void AddContact(string address)
        {
            Message.CC.Add(address);
        }

        public void SendMessage(string id, string pass)
        {
            if (Message == null)
            {
                throw new NullReferenceException("Unable to send message, Message is null");
            }

            if (string.IsNullOrEmpty(Message.Body))
            {
                throw new ArgumentException("Unable to send message, Message body is null or empty");
            }

            SmtpClient smtp = new SmtpClient(SMTPServer, PortNumber);
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(pass))
            {
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential(id, pass);
            }
            else
            {
                smtp.UseDefaultCredentials = false;
            }
            smtp.EnableSsl = true;
            smtp.SendCompleted += SendCompletedCallback;
            smtp.SendAsync(Message, "notification");
        }

        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                if (e.Error != null)
                {
                    Console.WriteLine("[{0}] {1}", e.UserState, e.Error);
                }
                else
                {
                    //_logger.Info("#Message sent to user");
                    Console.WriteLine("Message sent.");
                }
            }
            catch (SmtpException ex)
            {
                Console.WriteLine("!Exception caught: MESSAGE: " + ex.Message + "!Exception caught: EXCEPTION: " + ex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.WriteLine("!Exception caught: MESSAGE: " + ex.Message + "!Exception caught: EXCEPTION: " + ex.StackTrace);
            }
        }
    }
}
