using Prueba.Domain.PocoEntities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.Common.Interfaces.ProductInterface.Write
{
    public interface IProductWriteRepository
    {
        Task<AddProductResponse> CreateProduct(AddProductRequest request);

        Task<UpdateProductResponse> UpdateProduct(UpdateProductRequest request);

        Task<DeleteProductResponse> DeleteProduct(int id);
    }
}
