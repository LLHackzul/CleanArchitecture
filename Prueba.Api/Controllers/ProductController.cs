using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prueba.Application.Products.Commands.CreateProduct;
using Prueba.Application.Products.Commands.DeleteProduct;
using Prueba.Application.Products.Commands.UpdateProduct;
using Prueba.Application.Products.Queries;
using Prueba.Application.Validations.Products;
using Prueba.Domain.PocoEntities.Product;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Prueba.Api.Controllers
{
    [Route("api/Products")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IMediator mediator, ILogger<ProductController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                _logger.LogInformation("Se recibió una solicitud para obtener productos.");
                return Ok(await _mediator.Send(new GetProductsQuery())); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex," Error al obtener productos.");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] AddProductRequest request)
        {
            try
            {
                _logger.LogInformation("Se recibió una solicitud para crear un producto.");
                var validator = new CreateProductValidator();
                var validation = validator.Validate(request);
                if (!validation.IsValid)
                    throw new Exception(validation.ToString());
                return Ok(await _mediator.Send(new AddProductCommand(request)));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, " Error al crear producto.");
                return BadRequest(new { Error = ex.Message });
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateProduct([FromBody]  UpdateProductRequest request)
        {
            try
            {
                _logger.LogInformation("Se recibió una solicitud para actualizar un producto.");
                var validator = new UpdateProductValidator();
                var validation = validator.Validate(request);
                if (!validation.IsValid)
                    throw new Exception(validation.ToString());
                return Ok(await _mediator.Send(new UpdateProductCommand(request)));
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, " Error al actualizar producto.");
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromBody] DeleteProductCommand request)
        {
            try
            {

                _logger.LogInformation("Se recibió una solicitud para eliminar un producto.");
                return Ok(await _mediator.Send(request));
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, " Error al eliminar producto.");
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
