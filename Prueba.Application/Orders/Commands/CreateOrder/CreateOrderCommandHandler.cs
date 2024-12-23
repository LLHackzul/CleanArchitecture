using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Prueba.Application.Common.Interfaces;
using Prueba.Application.Common.Interfaces.OrderInterface.Write;
using Prueba.Domain.Entities;
using Prueba.Domain.PocoEntities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderResponse>
    {
        private readonly IOrderWriteRepository _orderRepository;
        private readonly ILogger<CreateOrderCommandHandler> _logger;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        public CreateOrderCommandHandler(IOrderWriteRepository orderRepository, ILogger<CreateOrderCommandHandler> logger)
        {
            _orderRepository = orderRepository;
            _logger = logger;
        }

        public async Task<CreateOrderResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            return await _orderRepository.CreateOrder(request.Model);
        }
    }
}
