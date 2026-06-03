using _4._1_Pedidos.Infraestructura.Data.DbContexto;
using _4._2_Pedidos.Infraestructura.Data.Contracts.Repositories.Abstracciones;
using _4._2_Pedidos.Infraestructura.Data.Contracts.Repositorios.Abstracciones;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._1_Pedidos.Infraestructura.Data.Repositorios
{
    public class DetallesRepositorio<T> : IDetallesRepositorio<T> where T : class
    {

        public DbContext contexto;

        public readonly DbSet<T> DbSet;

        public DetallesRepositorio(MasterDbContexto contexto)
        {
            this.contexto = contexto;
            this.DbSet = contexto.Set<T>();
        }


        public Task<T> createAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> deleteAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> existAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<T> getAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> getByIdAsync(params object[] keyvalues)
        {
            throw new NotImplementedException();
        }

        public Task<T> updateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        Task<List<T>> ILecturaRepositorio<T>.getAll()
        {
            throw new NotImplementedException();
        }

        public Task<T> deleteAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
