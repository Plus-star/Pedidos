using _4._2_Pedidos.Infraestructura.Data.Contracts.Repositories.Abstracciones;
using Pedidos.Aplicacion._2._1_Pedidos.Aplicacion.Core.Request;
using Pedidos.Aplicacion._2._1_Pedidos.Aplicacion.Core.Servicios.Abstracciones;
using Pedidos.Dominio.Core._3._2_Pedidos.Dominio.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Aplicacion._2._1_Pedidos.Aplicacion.Core.Servicios
{
    public class ServiciosPedidos : IServiciosPedidos
    {
        private readonly IPedidosRepositorio<pedidos> _pedidosRepositorio;

        public ServiciosPedidos(IPedidosRepositorio<pedidos> pedidosRepositorio)
        {
            _pedidosRepositorio = pedidosRepositorio;
        }

        public async Task<Boolean> ActualizarPedido(CrearActualizarPedidoRequest request)
        {
            var pedido = await _pedidosRepositorio.updateAsync(new pedidos
            {
                id_pedido = request.Id,
                fecha = request.Fecha
            });
            if (pedido != null) return true;
            else return false;
                
        }

        public async Task<pedidos> CrearPedido(CrearActualizarPedidoRequest request)
        {
            var pedido = await _pedidosRepositorio.createAsync(new pedidos
            {
                id_pedido = request.Id,
                fecha = request.Fecha
            });
            return pedido;

        }

        public Task<bool> EliminarPedido(int id)
        {
            var pedido = _pedidosRepositorio.deleteAsync(id);
            return pedido.ContinueWith(t =>
            {
                if (t.IsCompletedSuccessfully && t.Result != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            });
        }

        public Task<IEnumerable<pedidos>> FiltrarPedidos(string filtro)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<pedidos>> ListarPedidos()
        {
            return await _pedidosRepositorio.getAll();

        }

        public Task<pedidos> VisualizarPedido(int id)
        {
            throw new NotImplementedException();
        }
    }
}
