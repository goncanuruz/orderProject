using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Application.Abstractions.Services
{
    public interface IEmailService
    {
        public Task SendEmail(string email, string body, string subject);
    }
}
