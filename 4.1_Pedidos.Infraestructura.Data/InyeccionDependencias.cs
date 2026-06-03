using _4._1_Pedidos.Infraestructura.Data.DbContexto;
using _4._1_Pedidos.Infraestructura.Data.Repositorios;
using _4._2_Pedidos.Infraestructura.Data.Contracts.Repositories.Abstracciones;
using _4._2_Pedidos.Infraestructura.Data.Contracts.Repositorios.Abstracciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._1_Pedidos.Infraestructura.Data
{
    public static class InyeccionDependencias
    {

        public static IServiceCollection AddInfrastructureInjection(

            this IServiceCollection services,

            IConfiguration configuration)

        {


            //var useInMemory = false;
            //bool.TryParse(configuration["UseInMemory"], out useInMemory);

            //if (useInMemory)
            //{
            //    // Register in-memory repositories for development/testing without connecting to the DB
            //    services.AddScoped(typeof(IClientesRepositorio<>), typeof(Repositorios.InMemoryRepositorio<>));
            //    services.AddScoped(typeof(IPedidosRepositorio<>), typeof(Repositorios.InMemoryRepositorio<>));
            //    services.AddScoped(typeof(IDetallesRepositorio<>), typeof(Repositorios.InMemoryRepositorio<>));
            //}
            //else
            //{
                services.AddDbContext<MasterDbContexto>(cfg =>
                {
                    cfg.UseSqlServer(configuration["ConnectionStrings:cnxDBpedidos"]).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                });

                services.AddScoped(typeof(IClientesRepositorio<>), typeof(ClientesRepositorio<>));

                services.AddScoped(typeof(IPedidosRepositorio<>), typeof(PedidosRepositorio<>));

                services.AddScoped(typeof(IDetallesRepositorio<>), typeof(DetallesRepositorio<>));
            //}





            return services;

        }
    }
}
