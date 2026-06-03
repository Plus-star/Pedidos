using Microsoft.AspNetCore.Mvc;
using Pedidos.Aplicacion._2._1_Pedidos.Aplicacion.Core.Request;
using Pedidos.Aplicacion._2._1_Pedidos.Aplicacion.Core.Servicios.Abstracciones;
using Pedidos.Dominio.Core._3._2_Pedidos.Dominio.Core.Entidades;

namespace Pedidos.Api.Controllers
{

    [Route("api/[controller]")]

    [ApiController]
    public class ClientesController : Controller
    {
        private readonly IServiciosClientes _servicioClientes;

        public ClientesController(IServiciosClientes serviciosClientes)
        {
            _servicioClientes = serviciosClientes;
        }

        [HttpGet]
        public async Task<IEnumerable<clientes>> getClientes()
        {
            return await _servicioClientes.ListarClientes();
        }

        [HttpGet("{id}")]
        public async Task<clientes> getCliente(int id)
        {
            return await _servicioClientes.VisualizarCliente(id);
        }

        [HttpPost]
        public async Task<int> saveClientes([FromBody] CrearActualizarClienteRequest request)
        {
            var cliente  = await _servicioClientes.CrearCliente(request);
            return cliente.id_cliente;
        }

        [HttpPost]
        [Route("Actualizar")]
        public async Task<Boolean> updateCliente([FromBody] CrearActualizarClienteRequest request)
        {
            return await _servicioClientes.ActualizarCliente(request);
            
        }

        [HttpPost]
        [Route("Eliminar/{id}")]
        public async Task<Boolean> deleteCliente(int id)
        {
            return await _servicioClientes.EliminarCliente(id);
        }
    }
}
