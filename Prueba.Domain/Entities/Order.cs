using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba.Domain.Entities
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("IdOrder")]
        public int IdOrder { get; set; }

        public float Total { get; set; }

        public string ClientName { get; set; }


        public virtual ICollection<OrderDetail> OrderDetail { get; set; }= new List<OrderDetail>();
    }
}
