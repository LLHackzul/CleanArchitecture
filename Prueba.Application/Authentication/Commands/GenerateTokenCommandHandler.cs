using MediatR;
using Microsoft.IdentityModel.Tokens;
using Prueba.Application.Common.Configurations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace Prueba.Application.Authentication.Commands
{
    public class GenerateTokenCommandHandler : IRequestHandler<GenerateTokenCommand, string>
    {
        private readonly JwtSettings _jwtSettings;

        public GenerateTokenCommandHandler(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }


        public async Task<string> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
        {
            // Simular validación de usuario
            if (request.Username != "admin" || request.Password != "password")
                throw new UnauthorizedAccessException("Credenciales inválidas.");

            // Crear los claims del usuario
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, request.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            // Generar la clave secreta
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Crear el token
            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
