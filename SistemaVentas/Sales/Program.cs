using NServiceBus;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.Title = "Sales - Recibiendo Datos";

        var endpointConfiguration = new EndpointConfiguration("Sales");

        endpointConfiguration.UseSerialization<NewtonsoftJsonSerializer>();

        var transport = endpointConfiguration.UseTransport<LearningTransport>();
        transport.StorageDirectory(@"c:\LearningTransport");

        var endpointInstance = await Endpoint.Start(endpointConfiguration);

        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=================================");
        Console.WriteLine("   VENTAS: ESPERANDO ORDENES    ");
        Console.WriteLine("=================================");
        Console.ResetColor();

        Console.ReadLine();

        await endpointInstance.Stop();
    }
}