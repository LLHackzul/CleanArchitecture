using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Prueba.Application.Common.Interfaces;
using Prueba.Application.Common.Interfaces.ProductInterface.Write;
using Prueba.Domain.PocoEntities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.Products.Commands.DeleteProduct
{
    internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, DeleteProductResponse>
    {
        private readonly IProductWriteRepository _productRepository;
        private readonly ILogger<DeleteProductCommandHandler> _logger;
        public DeleteProductCommandHandler(IProductWriteRepository productRepository, ILogger<DeleteProductCommandHandler> logger)
        {
            _productRepository   = productRepository;
            _logger = logger;
        }
        public async Task<DeleteProductResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Procesando delete de un producto.");
            var response = await _productRepository.DeleteProduct(request.ProductId);

            _logger.LogInformation("Producto eliminado exitosamente.");
            return response;
        }
    }
}
