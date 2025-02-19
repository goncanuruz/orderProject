using OrderProject.Domain.Entities.Common;
using OrderProject.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Domain.Entities.Orders
{
    public class OrderDetail:BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }


        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
