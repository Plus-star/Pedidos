using _4._2_Pedidos.Infraestructura.Data.Contracts.Repositories.Abstracciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._2_Pedidos.Infraestructura.Data.Contracts.Repositorios.Abstracciones
{
    public interface IDetallesRepositorio<T>: ILecturaRepositorio<T>, IEscrituraRepositorio<T> where T : class
    {

    }
}
