using MediatR;
using Prueba.Domain.PocoEntities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<UpdateProductResponse>
    {
        public UpdateProductRequest Model { get; set; }

        public UpdateProductCommand(UpdateProductRequest model)
        {
            Model = model;
        }
    }
}
