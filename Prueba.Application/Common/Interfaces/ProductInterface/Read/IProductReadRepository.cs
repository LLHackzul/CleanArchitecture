using Prueba.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Application.Common.Interfaces.ProductInterface.Read
{
    public interface IProductReadRepository
    {
        Task<IEnumerable<GetProductDto>> GetProducts();
    }
}
