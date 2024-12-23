using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Domain.PocoEntities.Product
{
    public class UpdateProductRequest
    {
        public int ProductId { get; set; }
        public string Name { get; set; }

       public string Description { get; set; }

        public float Price { get; set; }

        public int Stock { get; set; }
    }
}
