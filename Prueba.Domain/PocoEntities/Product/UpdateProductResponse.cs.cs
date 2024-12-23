using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Domain.PocoEntities.Product
{
    public class UpdateProductResponse
    {
        public string result { get; set; }

        public static UpdateProductResponse Response(string result)
        {
            return new UpdateProductResponse { result = result };
        }
    }
}
