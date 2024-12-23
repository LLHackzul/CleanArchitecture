using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Domain.PocoEntities.Order
{
    public class CreateOrderRequest
    {
        public string ClientName { get; set; }
        public List<CreateOrderDetailRequest> OrderDetails { get; set; } = new();
    }
}
