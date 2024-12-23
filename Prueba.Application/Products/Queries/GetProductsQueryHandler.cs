using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Prueba.Application.Common.Interfaces;
using Prueba.Application.Common.Interfaces.ProductInterface.Read;
using Prueba.Application.Products.Commands;
using Prueba.Domain.DTOs;
using Prueba.Domain.Entities;
using Prueba.Domain.PocoEntities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.Products.Queries
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<GetProductDto>>
    {
        private readonly IProductReadRepository _productRepository;
        private readonly ILogger<GetProductsQueryHandler> _logger;
        public GetProductsQueryHandler(IProductReadRepository productRepository, ILogger<GetProductsQueryHandler> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }
        public async Task<IEnumerable<GetProductDto>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {

            _logger.LogInformation("Procesando listado de productos.");
            var response = await _productRepository.GetProducts();

            _logger.LogInformation("Productos listados exitosamente.");
            return response;
        }
    }
}
