using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Domain.PocoEntities.Product
{
    public class DeleteProductResponse
    {
        public string result { get; set; }

        public static DeleteProductResponse Response(string result)
        {
            return new DeleteProductResponse { result = result };
        }
    }
}
