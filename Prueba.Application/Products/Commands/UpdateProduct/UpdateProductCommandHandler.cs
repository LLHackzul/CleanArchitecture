using MediatR;
using Microsoft.Extensions.Logging;
using Prueba.Application.Common.Interfaces;
using Prueba.Application.Common.Interfaces.ProductInterface.Write;
using Prueba.Domain.Entities;
using Prueba.Domain.PocoEntities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductResponse>
    {
        private readonly IProductWriteRepository _productRepository;
        private readonly ILogger<UpdateProductCommandHandler> _logger;
        public UpdateProductCommandHandler(IProductWriteRepository productRepository, ILogger<UpdateProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        public async Task<UpdateProductResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Procesando actualizacion de un producto.");
            var product = new UpdateProductRequest
            {
                ProductId = request.Model.ProductId,
                Name = request.Model.Name,
                Description = request.Model.Description,
                Price = request.Model.Price,
                Stock = request.Model.Stock,
            };
            var response = await _productRepository.UpdateProduct(product);

            _logger.LogInformation("Producto actualizado exitosamente.");
            return response;
        }
    }
}
