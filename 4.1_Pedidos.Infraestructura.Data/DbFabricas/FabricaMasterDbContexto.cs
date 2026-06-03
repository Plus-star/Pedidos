using _4._1_Pedidos.Infraestructura.Data.DbContexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4._1_Pedidos.Infraestructura.Data.DbFabricas
{
    public class FabricaMasterDbContexto : IDesignTimeDbContextFactory<MasterDbContexto>
    {
        public MasterDbContexto CreateDbContext(string[] args)

        {

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Test";

            var basepath = Path.Combine(Directory.GetCurrentDirectory(), "..", "EnOpe.Api");


            var configuration = new ConfigurationBuilder()

                .SetBasePath(basepath)

                .AddJsonFile("appsettings.json")

                .AddJsonFile($"appsettings.{environment}.json", optional: true)

                .Build();


            var optionsBuilder = new DbContextOptionsBuilder<MasterDbContexto>();

            optionsBuilder.UseSqlServer(

                configuration["ConnectionStrings:cnxGHD"]);


            return new MasterDbContexto(optionsBuilder.Options);

        }

        MasterDbContexto IDesignTimeDbContextFactory<MasterDbContexto>.CreateDbContext(string[] args)
        {
            throw new NotImplementedException();
        }
    }
}
