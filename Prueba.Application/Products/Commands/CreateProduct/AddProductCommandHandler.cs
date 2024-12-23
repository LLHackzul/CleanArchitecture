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

namespace Prueba.Application.Products.Commands.CreateProduct
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, AddProductResponse>
    {
        private readonly IProductWriteRepository _productRepository;

        private readonly ILogger<AddProductCommandHandler> _logger;
        public AddProductCommandHandler(IProductWriteRepository productRepository, ILogger<AddProductCommandHandler> logger)
        {
            _productRepository = productRepository;
            _logger= logger;
        }

        public async Task<AddProductResponse> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Procesando creacion de nuevo producto.");
            var product = new AddProductRequest
            {
                Name = request.Model.Name,
                Description = request.Model.Description,
                Price = request.Model.Price,
                Stock = request.Model.Stock,
            };
            var response= await _productRepository.CreateProduct(product);

            _logger.LogInformation("Producto creado exitosamente.");
            return response;
        }
    }


}
