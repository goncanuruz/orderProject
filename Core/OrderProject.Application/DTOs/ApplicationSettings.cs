using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Application.DTOs
{
    public class ApplicationSettings
    {
        public EmailSettingDto EmailSettings { get; set; }
    }
    public class EmailSettingDto
    {
        public string DisplayName { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }
    }
}
