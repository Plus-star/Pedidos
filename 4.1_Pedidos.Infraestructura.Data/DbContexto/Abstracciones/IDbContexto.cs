using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace _4._1_Pedidos.Infraestructura.Data.DbContexto.Abstracciones
{
    public interface IDbContexto
    {
        DbSet <TEntidad> Set<TEntidad>() where TEntidad: class;
        DatabaseFacade Database {  get; }

        //DbSet<clientes> clientes { get; set; }
 


        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        void RemoveRange(IEnumerable<object> entities);

        EntityEntry Update(object entity);
    }
}

