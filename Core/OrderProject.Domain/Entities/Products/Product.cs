using OrderProject.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Domain.Entities.Products
{
    public class Product:BaseEntity
    {
        public string? Name { get; set; }
    }
}
