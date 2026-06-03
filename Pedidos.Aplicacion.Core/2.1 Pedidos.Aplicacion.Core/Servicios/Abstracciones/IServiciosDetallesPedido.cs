using Pedidos.Aplicacion._2._1_Pedidos.Aplicacion.Core.Request;
using Pedidos.Dominio.Core._3._2_Pedidos.Dominio.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Aplicacion._2._1_Pedidos.Aplicacion.Core.Servicios.Abstracciones
{
    public interface IServiciosDetallesPedido
    {
        Task<detalle_pedido> CrearDetalle(CrearActualizarDetalleRequest request);
        Task<Boolean> EliminarDetalle(int id);
        Task<Boolean> ActualizarDetalle(CrearActualizarDetalleRequest request);
        Task<pedidos> VisualizarDetalle(int id);
        Task<IEnumerable<detalle_pedido>> ListarDetalles();
        Task<IEnumerable<detalle_pedido>> FiltrarDetalles(string filtro);
    }
}
