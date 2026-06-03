using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Dominio.Core._3._2_Pedidos.Dominio.Core.Entidades
{

    public class clientes
    {
        [Key]
        public int id_cliente {  get; set; }
        public string? nombre { get; set; } 
        public string? email { get; set; }

        //public virtual ICollection<pedidos> Pedidos { get; set; } = new List<pedidos>();
    }
}
