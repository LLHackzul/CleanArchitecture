using MediatR;
using Prueba.Domain.PocoEntities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<DeleteProductResponse>
    {
        public int ProductId { get; set; }
    }
}
