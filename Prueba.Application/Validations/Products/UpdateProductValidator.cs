using FluentValidation;
using Prueba.Domain.PocoEntities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.Validations.Products
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductValidator() {
            RuleFor(x => x.ProductId)
                .NotNull()
                .WithMessage("El id del producto es obligatorio.")
                .GreaterThan(0)
                .WithMessage("El id del producto no es valido.");
            RuleFor(x => x.Name)
               .NotEmpty()
               .WithMessage("El nombre del producto es obligatorio.");
            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("La descripcion del producto es obligatoria.");
            RuleFor(x => x.Price)
                .NotEmpty()
                .WithMessage("El precio del producto es obligatorio.")
                .GreaterThan(0)
                .WithMessage("El precio del producto debe ser mayor a 0");
            RuleFor(x => x.Stock)
               .NotEmpty()
               .WithMessage("El Stock del producto es obligatorio.")
               .GreaterThan(0)
               .WithMessage("El Stock del producto debe ser mayor a 0");
        }
    }
}
