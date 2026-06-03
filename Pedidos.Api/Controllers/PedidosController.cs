using Microsoft.AspNetCore.Mvc;
using Pedidos.Aplicacion._2._1_Pedidos.Aplicacion.Core.Request;
using Pedidos.Aplicacion._2._1_Pedidos.Aplicacion.Core.Servicios;
using Pedidos.Aplicacion._2._1_Pedidos.Aplicacion.Core.Servicios.Abstracciones;
using Pedidos.Dominio.Core._3._2_Pedidos.Dominio.Core.Entidades;

namespace Pedidos.Api.Controllers
{

    [Route("api/[controller]")]

    [ApiController]
    public class PedidosController : Controller
    {
        private readonly IServiciosPedidos _servicioPedidos;

        //private readonly IServiciosDetallesPedido _serviciosDetallesPedidos;

        //public PedidosController(IServiciosPedidos serviciosPedidos, IServiciosDetallesPedido serviciosDetallesPedido)
        public PedidosController(IServiciosPedidos serviciosPedidos)
        {
            //_serviciosDetallesPedidos = serviciosDetallesPedido;
            _servicioPedidos = serviciosPedidos;
        }

        [HttpGet]
        public async Task<IEnumerable<pedidos>> getPedidos()
        {
            return await _servicioPedidos.ListarPedidos();
        }

        [HttpGet("{id}")]
        public async Task<pedidos> getPedido(int id)
        {
            return await _servicioPedidos.VisualizarPedido(id);
        }

        [HttpPost]
        public async Task<int> savePedidos([FromBody] CrearActualizarPedidoRequest request)
        {
            var pedido = await _servicioPedidos.CrearPedido(request);
            return pedido.id_pedido;
        }

        [HttpPost]
        [Route("Actualizar")]
        public async Task<Boolean> updatePedido([FromBody] CrearActualizarPedidoRequest request)
        {
            return await _servicioPedidos.ActualizarPedido(request);
        }

        [HttpPost]
        [Route("Eliminar")]
        public async Task<Boolean> deletePedido(int id)
        {
            return await _servicioPedidos.EliminarPedido(id);
        }
    }
}
