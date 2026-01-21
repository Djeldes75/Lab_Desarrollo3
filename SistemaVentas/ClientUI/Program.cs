using NServiceBus;
using Sales.Messages;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.Title = "CLIENTE - Cargando...";

        var endpointConfiguration = new EndpointConfiguration("ClientUI");

        endpointConfiguration.UseSerialization<NewtonsoftJsonSerializer>();

        var transport = endpointConfiguration.UseTransport<LearningTransport>();
        transport.StorageDirectory(@"c:\LearningTransport");

        var routing = transport.Routing();
        routing.RouteToEndpoint(typeof(PlaceOrder), "Sales");

        var endpointInstance = await Endpoint.Start(endpointConfiguration);

        Console.Clear();
        Console.Title = "CLIENTE - LISTO";
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("=================================");
        Console.WriteLine("   SISTEMA LISTO PARA USAR      ");
        Console.WriteLine("=================================");
        Console.ResetColor();
        Console.WriteLine("\nPresiona la tecla 'E' para enviar una orden...");

        while (true)
        {
            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.E)
            {
                Console.WriteLine("\n--> Enviando orden...");

                var command = new PlaceOrder
                {
                    OrderId = Guid.NewGuid(),
                    Descripcion = "Laptop Asus - Reparacion",
                    Precio = 2500.00m,
                    FechaIngreso = DateTime.Now,
                    TipoDocumento = "Cedula",
                    Documento = "403-1234567-1",
                    Nombres = "Altair",
                    Apellidos = "Lima"
                };

                await endpointInstance.Send(command);
                Console.WriteLine($"--> ¡Enviado! ID: {command.OrderId}");
                Console.WriteLine("Presiona 'E' para enviar otra.");
            }
        }
    }
}