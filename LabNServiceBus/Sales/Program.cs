using NServiceBus;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.Title = "Sales - PROCESADOR";
        Console.ForegroundColor = ConsoleColor.Cyan;

        var endpointConfiguration = new EndpointConfiguration("Sales");
        endpointConfiguration.UseSerialization<NewtonsoftJsonSerializer>();

        var transport = endpointConfiguration.UseTransport<LearningTransport>();
        transport.StorageDirectory(@"c:\LabNServiceBus_Data");

        endpointConfiguration.UsePersistence<LearningPersistence>();

        var endpointInstance = await Endpoint.Start(endpointConfiguration);

        Console.WriteLine("Sales esperando órdenes...");
        Console.ReadLine();
        await endpointInstance.Stop();
    }
}