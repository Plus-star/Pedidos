using Microsoft.Extensions.DependencyInjection;
using Pedidos.Aplicacion._2._1_Pedidos.Aplicacion.Core.Servicios;
using Pedidos.Aplicacion._2._1_Pedidos.Aplicacion.Core.Servicios.Abstracciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pedidos.Aplicacion
{
    public static class InyeccionDependencias
    {
        public static IServiceCollection AddApplicationInjection(

        this IServiceCollection services)

        {

            services.AddScoped<IServiciosClientes, ServiciosClientes>();

            services.AddScoped<IServiciosPedidos, ServiciosPedidos>();

            services.AddScoped<IServiciosDetallesPedido, ServiciosDetallesPedido>();


            return services;

        }
    }

}
