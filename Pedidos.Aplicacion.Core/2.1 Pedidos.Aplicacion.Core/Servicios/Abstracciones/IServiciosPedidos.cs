using Pedidos.Aplicacion._2._1_Pedidos.Aplicacion.Core.Request;
using Pedidos.Dominio.Core._3._2_Pedidos.Dominio.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Aplicacion._2._1_Pedidos.Aplicacion.Core.Servicios.Abstracciones
{
    public interface IServiciosPedidos
    {

        Task<pedidos> CrearPedido(CrearActualizarPedidoRequest request);
        Task<Boolean> EliminarPedido(int id);
        Task<Boolean> ActualizarPedido(CrearActualizarPedidoRequest request);
        Task<pedidos> VisualizarPedido(int id);
        Task<IEnumerable<pedidos>> ListarPedidos();
        Task<IEnumerable<pedidos>> FiltrarPedidos(string filtro);
    }
}
