using MediatR;
using Prueba.Domain.PocoEntities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<CreateOrderResponse>
    {
        public CreateOrderRequest Model { get; set; }

        public CreateOrderCommand(CreateOrderRequest model)
        {
            Model = model;
        }
    }
}
