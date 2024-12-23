using MediatR;
using Microsoft.EntityFrameworkCore;
using Prueba.Application.Common.Interfaces.ProductInterface.Read;
using Prueba.Application.Common.Interfaces.ProductInterface.Write;
using Prueba.Domain.DTOs;
using Prueba.Domain.Entities;
using Prueba.Domain.PocoEntities.Product;
using Prueba.Infrastructure.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Infrastructure.Repositories.ProductRepository
{
    public class ProductRepository : IProductWriteRepository, IProductReadRepository
    {
        private readonly DatabaseTestContext _context;

        public ProductRepository(DatabaseTestContext context) {  _context = context; }

        public async Task<IEnumerable<GetProductDto>> GetProducts()
        {
            var products = await _context.Products.ToListAsync();

            return products.Select(p => new GetProductDto
            {
                ProductId = p.ProductId,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock
            });
        }

        public async Task<AddProductResponse> CreateProduct(AddProductRequest request)
        {
            var existingProduct= await _context.Products.FirstOrDefaultAsync(p=>p.Name==request.Name);
            if (existingProduct != null)
                return AddProductResponse.Response("El producto ya existe.");
            Product newProduct = new Product
            {
                  Name = request.Name,
                  Description = request.Description,
                  Price = request.Price,
                  Stock = request.Stock
            };
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
            return AddProductResponse.Response("Creado correctamente.");
        }
        public async Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest request) 
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == request.ProductId);
            if (product != null)
            {
                product.Name = request.Name;
                product.Description = request.Description;
                product.Price = request.Price;
                product.Stock = request.Stock;
                _context.Products.Update(product);
                await _context.SaveChangesAsync();
                return UpdateProductResponse.Response("Producto actualizado correctamente.");
            }
            else
            {
                return UpdateProductResponse.Response("El Producto no existe.");
            }
        }

        public async Task<DeleteProductResponse> DeleteProduct(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return DeleteProductResponse.Response("Producto eliminado correctamente.");
            }
            else
            {
                return DeleteProductResponse.Response("El Producto no existe.");
            }
        }
    }
}
