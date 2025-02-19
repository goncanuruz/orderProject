using OrderProject.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Domain.Entities.Products
{
    public class Product:BaseEntity
    {
        [MaxLength(50)]
        public string? Name { get; set; }
        public string? Description { get; set; }
        [MaxLength(50)]
        public string? Category { get; set; }
        public double Unit { get; set; }
        public decimal UnitPrice { get; set; }
        public bool Status { get; set; }
    }
}
