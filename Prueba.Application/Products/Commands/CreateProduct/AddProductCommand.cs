using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Prueba.Domain.PocoEntities.Order;
using Prueba.Domain.PocoEntities.Product;
namespace Prueba.Application.Products.Commands.CreateProduct
{
    public class AddProductCommand : IRequest<AddProductResponse>
    {
        public AddProductRequest Model { get; set; }

        public AddProductCommand(AddProductRequest model)
        {
            Model = model;
        }
    }
}
