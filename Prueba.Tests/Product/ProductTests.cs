using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Prueba.Api.Controllers;
using Prueba.Application.Products.Commands.CreateProduct;
using Prueba.Application.Products.Commands.DeleteProduct;
using Prueba.Application.Products.Commands.UpdateProduct;
using Prueba.Domain.PocoEntities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Prueba.Tests.Product
{
    public class ProductTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly ProductController _controller;

        public ProductTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new ProductController(_mediatorMock.Object, Mock.Of<ILogger<ProductController>>());
        }

        [Fact]
        public async Task CreateProduct_WithValidResponse()
        {
            var expectedResponse = new AddProductResponse { result = "Creado correctamente." };
            var request = new AddProductRequest
            {
                    Name = "Product1",
                    Description = "Test Product",
                    Price = 100,
                    Stock = 10
             };

            _mediatorMock.Setup(m => m.Send(It.IsAny<AddProductCommand>(), default))
                         .ReturnsAsync(expectedResponse);

            var result = await _controller.CreateProduct(request);

            
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<AddProductResponse>(okResult.Value);

            Assert.Equal("Creado correctamente.", returnValue.result);
        }

        [Fact]
        public async Task UpdateProduct_WithValidResponse()
        {
            var expectedResponse = new UpdateProductResponse { result = "Producto actualizado correctamente." };
            var request =new UpdateProductRequest
            {
                    ProductId = 1,
                    Name = "Updated Product",
                    Description = "Test Product",
                    Price = 200,
                    Stock = 20
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<UpdateProductCommand>(), default))
                         .ReturnsAsync(expectedResponse);

            var result = await _controller.UpdateProduct(request);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<UpdateProductResponse>(okResult.Value);


            Assert.Equal("Producto actualizado correctamente.", returnValue.result);
        }

        [Fact]
        public async Task DeleteProduct_WhenProductIsDeleted()
        {

            var expectedResponse = new DeleteProductResponse { result = "Producto eliminado correctamente." };
            var command = new DeleteProductCommand { ProductId = 1 };

            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), default))
                         .ReturnsAsync(expectedResponse);

            var result = await _controller.DeleteProduct(command);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<DeleteProductResponse>(okResult.Value);

            Assert.Equal("Producto eliminado correctamente.", returnValue.result);
        }

        [Fact]
        public async Task DeleteProduct_WhenProductDoesNotExist()
        {

            var expectedResponse = new DeleteProductResponse { result = "El Producto no existe." };
            var command = new DeleteProductCommand { ProductId = 999 };

            _mediatorMock.Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), default))
                         .ReturnsAsync(expectedResponse);

            var result = await _controller.DeleteProduct(command);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<DeleteProductResponse>(okResult.Value);

            Assert.Equal("El Producto no existe.", returnValue.result);
        }
    }
}
