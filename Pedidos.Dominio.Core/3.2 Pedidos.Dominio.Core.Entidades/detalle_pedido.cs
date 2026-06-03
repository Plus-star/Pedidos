using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Dominio.Core._3._2_Pedidos.Dominio.Core.Entidades
{
    public class detalle_pedido
    {

        [Key]
        public int id_detalle { get; set; }

        [ForeignKey ("id_pedido")]
        public int? id_pedido { get; set; }

        public string? producto { get; set; }

        public int? cantidad { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? precio { get; set; }

        public virtual pedidos? pedido { get; set; }

    }
}
