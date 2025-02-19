using OrderProject.Domain.Entities.Common;
using OrderProject.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Domain.Entities.Orders
{
    public class OrderDetail:BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public decimal UnitPrice { get; set; }


        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
