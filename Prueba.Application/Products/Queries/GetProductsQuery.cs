using MediatR;
using Prueba.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.Products.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<GetProductDto>>;
}
