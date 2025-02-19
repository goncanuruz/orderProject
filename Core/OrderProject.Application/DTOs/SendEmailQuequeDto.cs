using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Application.DTOs
{
    public class SendEmailQuequeDto
    {
        public string Email { get; set; }
        public string Content { get; set; }
        public string Subject { get; set; }
    }
}
