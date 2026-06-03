using Pedidos.Aplicacion;
using _4._1_Pedidos.Infraestructura.Data;
using _4._1_Pedidos.Infraestructura.Data.Repositorios;
using _4._2_Pedidos.Infraestructura.Data.Contracts.Repositories.Abstracciones;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddApplicationInjection();
builder.Services.AddInfrastructureInjection(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddPolicy("_myOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsEnvironment("Test"))
{
    app.UseSwagger();
    //app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Pedidos.Api 1.0"); options.RoutePrefix = string.Empty; });
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("_myOrigins");
app.UseCookiePolicy();
//app.UseCors("EnableCORS");





app.UseAuthorization();


app.MapControllers();
 

app.Run();
