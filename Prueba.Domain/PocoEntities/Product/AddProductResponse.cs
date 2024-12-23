using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Domain.PocoEntities.Product
{
    public class AddProductResponse
    {
        public string result { get; set; }

        public static AddProductResponse Response(string result)
        {
            return new AddProductResponse { result = result };
        }
    }
}
