using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prueba.Application.Orders.Commands.CreateOrder;
using Prueba.Application.Validations.Orders;
using Prueba.Application.Validations.Products;
using Prueba.Domain.PocoEntities.Order;

namespace Prueba.Api.Controllers
{
    [Route("api/Order")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        private readonly ILogger<OrderController> _logger;

        public OrderController(IMediator mediator, ILogger<OrderController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            try
            {

                _logger.LogInformation("Se recibió una solicitud para realizar una orden.");
                var validator = new CreateOrderValidator();
                var validation = validator.Validate(request);
                if (!validation.IsValid)
                    return BadRequest(new { Errors = validation.Errors.Select(e => e.ErrorMessage) });
                var response = await _mediator.Send(new CreateOrderCommand(request));
                return Ok(response);
            }
            catch (Exception ex)
            {

                _logger.LogError(ex, " Error al procesar la orden.");
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
