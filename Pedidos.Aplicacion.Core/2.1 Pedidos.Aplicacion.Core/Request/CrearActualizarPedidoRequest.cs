using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Aplicacion._2._1_Pedidos.Aplicacion.Core.Request
{
    public class CrearActualizarPedidoRequest
    {
        public int Id { get; set; }
        public DateOnly? Fecha { get; set; }
    }
}
