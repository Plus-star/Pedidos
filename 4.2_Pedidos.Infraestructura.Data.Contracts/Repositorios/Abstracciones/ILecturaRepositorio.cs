using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._2_Pedidos.Infraestructura.Data.Contracts.Repositories.Abstracciones
{
    public interface ILecturaRepositorio<T> where T : class
    {
        Task<List<T>> getAll();
        Task<T> getByIdAsync(params object[] keyvalues);
    }
}
