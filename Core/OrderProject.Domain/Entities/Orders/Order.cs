using OrderProject.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Domain.Entities.Orders
{
    public class Order:BaseEntity
    {
        [MaxLength(50)]
        public string CustomerName { get; set; }
        [MaxLength(50)]
        public string CustomerEmail { get; set; }
        [MaxLength(50)]
        public string CustomerGsm { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
