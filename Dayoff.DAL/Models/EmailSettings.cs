using System;
using System.Collections.Generic;
using System.Text;

namespace Dayoff.DAL.Models
{
    public class EmailSettings
    {
        public string SmtpServer { get; set; }
        public int SmtpPortNumber { get; set; }
        public string SenderName { get; set; }
        public string SenderEmail { get; set; }
        public string SuperAdmin { get; set; }
        public string Id { get; set; }
        public string Pass { get; set; }
        public string WebAddress { get; set; }
    }
}
