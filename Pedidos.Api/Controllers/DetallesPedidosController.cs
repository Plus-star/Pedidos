using Microsoft.AspNetCore.Mvc;
using Pedidos.Aplicacion._2._1_Pedidos.Aplicacion.Core.Request;
using Pedidos.Aplicacion._2._1_Pedidos.Aplicacion.Core.Servicios.Abstracciones;
using Pedidos.Dominio.Core._3._2_Pedidos.Dominio.Core.Entidades;

namespace Pedidos.Api.Controllers
{

    [Route("api/[controller]")]

    [ApiController]
    public class DetallesPedidosController : Controller
    {
        private readonly IServiciosDetallesPedido _servicioDetallesPedido;

        public DetallesPedidosController(IServiciosDetallesPedido serviciosDetallesPedido)
        {
            _servicioDetallesPedido = serviciosDetallesPedido;
        }

        [HttpGet]
        public async Task<IEnumerable<detalle_pedido>> getDetalles()
        {
            return await _servicioDetallesPedido.ListarDetalles();
        }

        [HttpGet("{id}")]
        public async Task<pedidos> getDetalle(int id)
        {
            return await _servicioDetallesPedido.VisualizarDetalle(id);
            
        }

        [HttpPost]
        public async Task<int> saveDetalles([FromBody] CrearActualizarDetalleRequest request)
        {
            var detalle = await _servicioDetallesPedido.CrearDetalle(request);
            return detalle.id_detalle;
        }

        [HttpPost]
        [Route("Actualizar")]
        public async Task<Boolean> updateDetalle([FromBody] CrearActualizarDetalleRequest request)
        {
            return await _servicioDetallesPedido.ActualizarDetalle(request);
        }

        [HttpPost]
        [Route("Eliminar")]
        public async Task<Boolean> deleteDetalle(int id)
        {
            return await _servicioDetallesPedido.EliminarDetalle(id);
        }
    }
}
