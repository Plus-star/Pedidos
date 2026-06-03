using _4._1_Pedidos.Infraestructura.Data.DbContexto;
using _4._2_Pedidos.Infraestructura.Data.Contracts.Repositories.Abstracciones;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._1_Pedidos.Infraestructura.Data.Repositorios
{
    public class ClientesRepositorio<T> : IClientesRepositorio<T> where T : class
    {
        public DbContext contexto;

        public readonly DbSet<T> DbSet;

        public ClientesRepositorio(MasterDbContexto contexto)
        {
            this.contexto = contexto;
            this.DbSet = contexto.Set<T>();
        }

        public async Task<T> createAsync(T entity)
        {
            DbSet.Add(entity);
            await contexto.SaveChangesAsync();
            return entity;
        }

        public async Task<T> deleteAsync(int id)
        {
            var entity = await DbSet.FindAsync(id);
            if (entity == null)
            return null;
            
            DbSet.Remove(entity);
            await contexto.SaveChangesAsync();
            return entity;
        }

        public Task<T> existAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> getAll()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T> getByIdAsync(params object[] keyvalues)
        {
            return await DbSet.FindAsync(keyvalues);
        }

        public async Task<T> updateAsync(T entity)
        {
            contexto.Entry(entity).State = EntityState.Modified;
            await contexto.SaveChangesAsync();
            return entity;
        }

    }
}
