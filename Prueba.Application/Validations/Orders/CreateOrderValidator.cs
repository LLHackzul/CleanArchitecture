using FluentValidation;
using Prueba.Domain.PocoEntities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.Validations.Orders
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderRequest>
    {
        public CreateOrderValidator() {
            RuleFor(x => x.ClientName)
                    .NotEmpty().WithMessage("El nombre del cliente es obligatorio.")
                    .MaximumLength(100).WithMessage("El nombre del cliente no puede superar los 100 caracteres.");

            RuleFor(x => x.OrderDetails)
                .NotEmpty().WithMessage("La orden debe tener al menos un detalle.");

            RuleForEach(x => x.OrderDetails).ChildRules(detail =>
            {
                detail.RuleFor(d => d.ProductId)
                    .GreaterThan(0).WithMessage("El ID del producto debe ser mayor que 0.");

                detail.RuleFor(d => d.Quantity)
                   .GreaterThan((short)0).WithMessage("La cantidad debe ser mayor que 0.")
                   .LessThanOrEqualTo(short.MaxValue).WithMessage($"La cantidad no puede superar el máximo permitido ({short.MaxValue}).");
            });
        }
    }
}
