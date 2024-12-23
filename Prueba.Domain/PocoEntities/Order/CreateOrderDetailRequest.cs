using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Domain.PocoEntities.Order
{
    public class CreateOrderDetailRequest
    {
        public int ProductId { get; set; }
        public short Quantity { get; set; }
    }
}
