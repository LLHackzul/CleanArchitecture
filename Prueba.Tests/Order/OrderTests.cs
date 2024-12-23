using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Prueba.Api.Controllers;
using Prueba.Application.Orders.Commands.CreateOrder;
using Prueba.Domain.PocoEntities.Order;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Prueba.Tests.Order
{
    public class OrderTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly OrderController _controller;

        public OrderTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new OrderController(_mediatorMock.Object, Mock.Of<ILogger<OrderController>>());
        }

        [Fact]
        public async Task CreateOrder_ReturnsOk_WhenOrderIsCreatedSuccessfully()
        {
            var expectedResponse = new CreateOrderResponse
            {
                OrderId = 1,
                Total = 500,
                Message = "Orden creada exitosamente."
            };

            var command = new CreateOrderRequest
            {
                ClientName = "Test Client",
                OrderDetails = new List<CreateOrderDetailRequest>
                {
                    new CreateOrderDetailRequest { ProductId = 1, Quantity = 5 }
                }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateOrderCommand>(), default))
                         .ReturnsAsync(expectedResponse);

            var result = await _controller.CreateOrder(command);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<CreateOrderResponse>(okResult.Value);

            Assert.Equal(1, returnValue.OrderId);
            Assert.Equal(500, returnValue.Total);
            Assert.Equal("Orden creada exitosamente.", returnValue.Message);
        }

        [Fact]
        public async Task CreateOrder_ReturnsResponse_WhenProductDoesNotExist()
        {
            var command = new CreateOrderRequest
            {
                ClientName = "Test Client",
                OrderDetails = new List<CreateOrderDetailRequest>
                {
                    new CreateOrderDetailRequest { ProductId = 99, Quantity = 5 }
                }  
            };

            var expectedResponse = new CreateOrderResponse
            {
                OrderId = 0,
                Total = 0,
                Message = "El producto con ID 99 no existe."
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateOrderCommand>(), default))
                         .ReturnsAsync(expectedResponse);

            var result = await _controller.CreateOrder(command);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<CreateOrderResponse>(okResult.Value);

            Assert.Equal(expectedResponse.OrderId, returnValue.OrderId);
            Assert.Equal(expectedResponse.Total, returnValue.Total);
            Assert.Equal(expectedResponse.Message, returnValue.Message);
        }

        [Fact]
        public async Task CreateOrder_ReturnsResponse_WhenStockIsInsufficient()
        {
            var command = new CreateOrderRequest
            {
                ClientName = "Test Client",
                OrderDetails = new List<CreateOrderDetailRequest>
                {
                    new CreateOrderDetailRequest { ProductId = 1, Quantity = 100 } // Stock insuficiente
                }
            };

            var expectedResponse = new CreateOrderResponse
            {
                OrderId = 0,
                Total = 0,
                Message = "El producto con ID 1 no tiene suficiente stock."
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateOrderCommand>(), default))
                         .ReturnsAsync(expectedResponse);

            var result = await _controller.CreateOrder(command);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<CreateOrderResponse>(okResult.Value);

            Assert.Equal(expectedResponse.OrderId, returnValue.OrderId);
            Assert.Equal(expectedResponse.Total, returnValue.Total);
            Assert.Equal(expectedResponse.Message, returnValue.Message);
        }
    }
}
