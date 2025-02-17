using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Application.DTOs.Controller
{
    public class ControllerPermissionDto
    {
        public string? List { get; set; }
        public string? Detail { get; set; }
        public string? Edit { get; set; }
        public string? Create { get; set; }
        public string? Delete { get; set; }
    }
}
