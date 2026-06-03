using _4._1_Pedidos.Infraestructura.Data.DbContexto.Abstracciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Pedidos.Dominio.Core._3._2_Pedidos.Dominio.Core.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._1_Pedidos.Infraestructura.Data.DbContexto
{
    public class MasterDbContexto : DbContext, IDbContexto
    {

        public MasterDbContexto(DbContextOptions<MasterDbContexto> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)



        {


            if (!optionsBuilder.IsConfigured)

            {

                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");


                var configuration = new ConfigurationBuilder()

                                        .SetBasePath(Directory.GetCurrentDirectory())

                                        .AddJsonFile("appsettings.json", optional: false)

                                        .AddJsonFile($"appsettings.{environment}.json", optional: true)

                                        .Build();


                optionsBuilder.UseSqlServer(configuration["ConnectionStrings:cnxDBpedidos"]).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

            // 1. Configuración de CLIENTES
            modelBuilder.Entity<clientes>(entity =>
            {
                entity.ToTable("clientes");
                entity.HasKey(c => c.id_cliente);
            });


            //2. Configuración de PEDIDOS
            modelBuilder.Entity<pedidos>().ToTable("pedidos").HasOne(p => p.cliente).WithMany().HasForeignKey(p => p.id_cliente);
            //modelBuilder.Entity<pedidos>().ToTable("pedidos").HasOne<clientes>().WithMany().HasForeignKey(p => p.id_cliente);


            //2.Configuración de PEDIDOS
            //modelBuilder.Entity<pedidos>(entity =>
            //{
            //    entity.ToTable("pedidos");
            //    entity.HasKey(p => p.id_pedido);

            //    // Relación: Clientes (1) <---> Pedidos (N)
            //    entity.HasOne(p => p.cliente)
            //        .WithMany(C => C.Pedidos)
            //        .HasForeignKey(p => p.id_cliente)
            //        .OnDelete(DeleteBehavior.Restrict);
            //});

            //3. Configuración de DETALLE_PEDIDO
            modelBuilder.Entity<detalle_pedido>().ToTable("detalle_pedido").HasOne(p => p.pedido).WithMany().HasForeignKey(p => p.id_pedido);
            
            //3. Configuración de DETALLE_PEDIDO
            //modelBuilder.Entity<detalle_pedido>(entity =>
            //{
            //    entity.ToTable("detalle_pedido");
            //    entity.HasKey(dp => dp.id_detalle);

            //    // Relación: Pedidos (1) <---> Detalle_Pedido (N)
            //    entity.HasOne(dp => dp.pedido)
            //          .WithMany(p => p.Detalles)
            //          .HasForeignKey(dp => dp.id_pedido)
            //          .OnDelete(DeleteBehavior.Cascade);
            //});
            







        }

        //public DatabaseFacade Database => throw new NotImplementedException();

        //public void RemoveRange(IEnumerable<object> entities)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    throw new NotImplementedException();
        //}

        //public DbSet<TEntidad> Set<TEntidad>() where TEntidad : class
        //{
        //    throw new NotImplementedException();
        //}

        //public EntityEntry Update(object entity)
        //{
        //    throw new NotImplementedException();
        //}

    }
}
