using Prueba.Domain.PocoEntities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.Common.Interfaces.OrderInterface.Write
{
    public interface IOrderWriteRepository
    {
        Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request); 
    }
}
