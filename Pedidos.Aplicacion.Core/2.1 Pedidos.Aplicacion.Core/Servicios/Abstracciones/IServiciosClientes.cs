using Pedidos.Aplicacion._2._1_Pedidos.Aplicacion.Core.Request;
using Pedidos.Dominio.Core._3._2_Pedidos.Dominio.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Aplicacion._2._1_Pedidos.Aplicacion.Core.Servicios.Abstracciones
{
    public interface IServiciosClientes
    {
        Task<clientes> CrearCliente(CrearActualizarClienteRequest request);
        Task<Boolean> EliminarCliente(int id);
        Task<Boolean> ActualizarCliente(CrearActualizarClienteRequest request);
        Task<clientes> VisualizarCliente(int id);
        Task<IEnumerable<clientes>> ListarClientes();
        Task<IEnumerable<clientes>> FiltrarClientes(string filtro);
        

    }
}
