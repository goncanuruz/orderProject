using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProject.Application.ViewModels.Orders
{
    public class CreateOrderRequest
    {
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerGsm { get; set; }

        public List<CreateOrderDetail> OrderDetails { get; set; }
    }
}
