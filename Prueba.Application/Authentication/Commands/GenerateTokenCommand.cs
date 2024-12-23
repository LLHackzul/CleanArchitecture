using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.Authentication.Commands
{
    public class GenerateTokenCommand : IRequest<string>
    {
        public string Username { get; set; }

        public string Password { get; set; }
    }
}
