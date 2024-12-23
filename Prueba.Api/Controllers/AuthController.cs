using MediatR;
using Microsoft.AspNetCore.Mvc;
using Prueba.Application.Authentication.Commands;

namespace Prueba.Api.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("token")]
        public async Task<IActionResult> GenerateToken([FromBody] GenerateTokenCommand command)
        {
            try
            {
                var token = await _mediator.Send(command);
                return Ok(new { Token = token });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { Message = "Credenciales inválidas." });
            }
        }
    }
}
