using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Dominio.Core._3._2_Pedidos.Dominio.Core.Entidades
{
    public class pedidos
    {


        [Key]
        public int id_pedido { get; set; }

        [ForeignKey("id_cliente")]
        public virtual clientes cliente { get; set; }

        public int id_cliente { get; set; }

        public DateOnly? fecha { get; set; }

        //public virtual ICollection<detalle_pedido> Detalles { get; set; } = new List<detalle_pedido>();


    }
}
