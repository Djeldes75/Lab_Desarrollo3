using DenunciasService.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=DB_Denuncias;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;";

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();
app.MapControllers();

_ = Task.Run(async () =>
{

    await Task.Delay(2000);

    Console.Clear();

    Console.Title = "SERVIDOR DE APLICACIONES - SISTEMA INTEC v2.0";

    Console.ForegroundColor = ConsoleColor.Cyan;

    Console.WriteLine(@"
    +--------------------------------------------------------------+
    |           SISTEMA DE DENUNCIAS - API GATEWAY                 |
    |           ESTADO: EN LINEA | PUERTO: 5062                    |
    +--------------------------------------------------------------+
    ");

    Console.ForegroundColor = ConsoleColor.Green;

    Console.WriteLine("    [OK] Base de Datos SQL ......... CONECTADA");
    Console.WriteLine("    [OK] Interfaz Web (Front) ...... http://localhost:5062");
    Console.WriteLine("    [OK] Swagger (Docs) ............ http://localhost:5062/swagger");
    Console.WriteLine("    --------------------------------------------------");

    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine("\n    [i] Esperando transacciones en tiempo real...\n");
    Console.ResetColor();
});

app.Run();