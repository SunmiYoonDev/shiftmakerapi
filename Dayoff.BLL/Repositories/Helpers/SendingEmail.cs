using Dayoff.BLL.Services;
using Dayoff.DAL.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Dayoff.BLL.Repositories.Helpers
{
    public class SendingEmail:ISendingEmail
    {
        private readonly IOptions<EmailSettings> _configs;
        private IHostingEnvironment _env;

        public SendingEmail(IOptions<EmailSettings> Configs,
           IHostingEnvironment env)
        {
            _configs = Configs;
            _env = env;
        }

        public void SendEmail(Guid? id, string key, string email, string function)
        {
            //From Address  
            string FromAddress = _configs.Value.SenderEmail;
            string FromAddressTitle = _configs.Value.SenderName;
            //To Address  
            string ToAddress = email;
            string ToAddressTitle = "Shift manager Client";

            string Subject = "";
            string BodyContent = "";
            var emailPath = "";
            var builder = new BodyBuilder();
            var webAddress = "";

            if (function.Equals("forgotpassword"))
            {
                Subject = "Reset your Shift manager password.";
                emailPath = Path.Combine("./",
                                    "Templates/",
                                    "EmailTemplate/",
                                    "Change_Password.html");
                using (StreamReader SourceReader = System.IO.File.OpenText(emailPath))
                {
                    builder.HtmlBody = SourceReader.ReadToEnd();
                }
                webAddress = _configs.Value.WebAddress + "/" + id + "/" + key;
                BodyContent = string.Format(builder.HtmlBody,
                       email,
                       webAddress);
            }

            //Smtp Server
            string SmtpServer = _configs.Value.SmtpServer;
            //Smtp Port Number  
            int SmtpPortNumber = _configs.Value.SmtpPortNumber;

            EmailService emailService = new EmailService(SmtpServer, SmtpPortNumber);
            emailService.CreateMessage(ToAddressTitle, ToAddress, FromAddressTitle, FromAddress, Subject);
            emailService.SetMessageBody(BodyContent);
            emailService.SendMessage(_configs.Value.Id, _configs.Value.Pass);

        }
    }
}
