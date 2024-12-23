using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Domain.PocoEntities.Order
{
    public class CreateOrderResponse
    {
        public int OrderId { get; set; }
        public float Total { get; set; }
        public string Message { get; set; }
    }
}
