using _4._2_Pedidos.Infraestructura.Data.Contracts.Repositorios.Abstracciones;
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
    public class ServiciosDetallesPedido : IServiciosDetallesPedido
    {
        private readonly IDetallesRepositorio<detalle_pedido> _detallesRepositorio;

        public ServiciosDetallesPedido(IDetallesRepositorio<detalle_pedido> detallesRepositorio)
        {
            _detallesRepositorio = detallesRepositorio;
        }

        public async Task<Boolean> ActualizarDetalle(CrearActualizarDetalleRequest request)
        {
            var detalle = await _detallesRepositorio.updateAsync(new detalle_pedido
            {
                id_detalle = request.Id,
                producto = request.Producto,
                cantidad = request.Cantidad,
                precio = request.Precio
            });
            if (detalle != null) return true;
            else return false;
        }

        public async Task<detalle_pedido> CrearDetalle(CrearActualizarDetalleRequest request)
        {
            var detalle = await _detallesRepositorio.createAsync(new detalle_pedido
            {
                id_detalle = request.Id,
                producto = request.Producto,
                cantidad = request.Cantidad,
                precio = request.Precio
            });
            return detalle;
        }

        public Task<Boolean> EliminarDetalle(int id)
        {
            var detalle = _detallesRepositorio.deleteAsync(id);
            return detalle.ContinueWith(t =>
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

        public Task<IEnumerable<detalle_pedido>> FiltrarDetalles(string filtro)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<detalle_pedido>> ListarDetalles()
        {
            return await _detallesRepositorio.getAll();
        }

        public Task<pedidos> VisualizarDetalle(int id)
        {
            throw new NotImplementedException();
        }


    }
}
