using OrderProject.Application.DTOs.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Application.ViewModels.Products
{
    public class UpdateProductDto:IUpdateDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Category { get; set; }
        public double Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public bool Status { get; set; }
    }
}
