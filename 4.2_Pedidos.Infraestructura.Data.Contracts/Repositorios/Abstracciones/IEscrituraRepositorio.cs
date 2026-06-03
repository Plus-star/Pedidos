using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._2_Pedidos.Infraestructura.Data.Contracts.Repositories.Abstracciones
{
    public interface IEscrituraRepositorio<T> where T : class
    {
        Task<T> createAsync(T entity);
        Task<T> updateAsync(T entity);
        Task<T> deleteAsync(int id);
        Task<T> existAsync(int id);
    }
}
