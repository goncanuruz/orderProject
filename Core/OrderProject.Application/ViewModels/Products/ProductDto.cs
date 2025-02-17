using OrderProject.Application.DTOs.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Application.ViewModels.Products
{
    public class ProductDto:IListDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
    }
}
