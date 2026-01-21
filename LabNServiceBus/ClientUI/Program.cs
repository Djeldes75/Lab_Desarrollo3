using NServiceBus;
using Sales.Messages;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.Title = "ClientUI - ENVIAR ORDENES";
        Console.ForegroundColor = ConsoleColor.Green;

        var endpointConfiguration = new EndpointConfiguration("ClientUI");
        endpointConfiguration.UseSerialization<NewtonsoftJsonSerializer>();

        var transport = endpointConfiguration.UseTransport<LearningTransport>();
        transport.StorageDirectory(@"c:\LabNServiceBus_Data");

        var routing = transport.Routing();
        routing.RouteToEndpoint(typeof(PlaceOrder), "Sales");

        var endpointInstance = await Endpoint.Start(endpointConfiguration);

        Console.Clear();
        Console.WriteLine("========================================");
        Console.WriteLine("   CLIENT UI - SISTEMA DE PEDIDOS");
        Console.WriteLine("========================================");
        Console.WriteLine("Presiona [ENTER] para enviar una orden...");

        while (true)
        {
            Console.ReadLine();

            var orden = new PlaceOrder
            {
                OrderId = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                Descripcion = "Laptop MSI Raider GE86HX",
                Precio = 2510.00m,
                FechaIngreso = DateTime.Now,
                TipoDocumento = "Cedula",
                Documento = "403-1234567-1",
                Nombres = "Zero",
                Apellidos = "Liron"
            };

            await endpointInstance.Send(orden);
            Console.WriteLine($"[✔] Orden {orden.OrderId} enviada exitosamente.");
        }
    }
}