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

    public class ServiciosClientes : IServiciosClientes
    {
        private readonly IClientesRepositorio<clientes> _clientesRepositorio; 

        public ServiciosClientes(IClientesRepositorio<clientes> clientesRepositorio)
        {
            _clientesRepositorio = clientesRepositorio;
        }

        public async Task<Boolean> ActualizarCliente(CrearActualizarClienteRequest request)
        {
            var cliente = await _clientesRepositorio.updateAsync(new clientes
            {
                id_cliente = request.Id,
                nombre = request.Nombre,
                email = request.Email

            });
            if (cliente != null) return true;
            else return false;

        }

        public async Task<clientes> CrearCliente(CrearActualizarClienteRequest request)
        {
            var cliente = await _clientesRepositorio.createAsync(new clientes
            {
                id_cliente = request.Id,
                nombre = request.Nombre,
                email = request.Email

            });
            return cliente;
        }

        public Task<bool> EliminarCliente(int id)
        {
            var cliente = _clientesRepositorio.deleteAsync(id);
            return cliente.ContinueWith(t =>
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

        public Task<IEnumerable<clientes>> FiltrarClientes(string filtro)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<clientes>> ListarClientes()
        {
            return await _clientesRepositorio.getAll();

        }

        public Task<clientes> VisualizarCliente(int id)
        {
            throw new NotImplementedException();
        }
    }
}
