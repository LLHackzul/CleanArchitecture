using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Prueba.Application.Common.Interfaces.OrderInterface.Write;
using Prueba.Application.Orders.Commands.CreateOrder;
using Prueba.Domain.Entities;
using Prueba.Domain.PocoEntities.Order;
using Prueba.Infrastructure.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Prueba.Infrastructure.Repositories.OrderRepository
{
    public class OrderRepository : IOrderWriteRepository
    {
        private readonly DatabaseTestContext _context;

        private readonly ILogger<CreateOrderCommandHandler> _logger;
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
        public OrderRepository(DatabaseTestContext context, ILogger<CreateOrderCommandHandler> logger) { _context = context; _logger = logger; }

        public async Task<CreateOrderResponse> CreateOrder(CreateOrderRequest request)
        {
            await _semaphore.WaitAsync();
            _logger.LogInformation("Procesando una nueva orden para el cliente {ClientName}.", request.ClientName);
            using var transaction = await _context.BeginTransactionAsync();
            try
            {
                _logger.LogInformation("obteniendo el id de los productos de la orden.");
                var productIds = request.OrderDetails.Select(d => d.ProductId).ToList();
                _logger.LogInformation("obteniendo los productos de la orden.");
                var products = await _context.Products
                    .Where(p => productIds.Contains(p.ProductId))
                    .ToListAsync();
                _logger.LogInformation("Validando los productos de la orden.");
                foreach (var detail in request.OrderDetails)
                {
                    var product = products.FirstOrDefault(p => p.ProductId == detail.ProductId);
                    _logger.LogInformation("Despues de buscar el producto se valida si existe o tiene stock.");
                    if (product == null)
                    {
                        _logger.LogInformation("El producto con ID {ProductId} no existe.", detail.ProductId);
                        return new CreateOrderResponse
                        {
                            OrderId = 0,
                            Total = 0,
                            Message = $"El producto con ID {detail.ProductId} no existe."
                        };
                    }
                    _logger.LogInformation("El producto existe, validando stock.");
                    if (product.Stock < detail.Quantity)
                    {
                        _logger.LogInformation("El producto con ID {ProductId} no tiene suficiente stock.", detail.ProductId);
                        return new CreateOrderResponse
                        {
                            OrderId = 0,
                            Total = 0,
                            Message = $"El producto con ID {detail.ProductId} no tiene suficiente stock."
                        };
                    }

                    _logger.LogInformation("Cuenta con Stock");
                }

                _logger.LogInformation("Creando la orden.");
                var order = new Order
                {
                    ClientName = request.ClientName,
                    Total = 0
                };

                _context.Orders.Add(order);

                _logger.LogInformation("Actualizando el stock de los productos y creando el detalle de la orden.");
                float total = 0;
                foreach (var detail in request.OrderDetails)
                {
                    var product = products.First(p => p.ProductId == detail.ProductId);

                    var subtotal = product.Price * detail.Quantity;

                    order.OrderDetail.Add(new OrderDetail
                    {
                        ProductId = product.ProductId,
                        Quantity = detail.Quantity,
                        Subtotal = subtotal
                    });

                    product.Stock -= detail.Quantity;

                    total += subtotal;
                }
                order.Total = total;

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                _logger.LogInformation("Orden procesada exitosamente.");
                return new CreateOrderResponse
                {
                    OrderId = order.IdOrder,
                    Total = order.Total,
                    Message = "Orden creada exitosamente."
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al procesar la orden.");
                await transaction.RollbackAsync();
                throw new Exception($"Error al crear la orden: {ex.Message}");
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}
