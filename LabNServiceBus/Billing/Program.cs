using NServiceBus;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.Title = "Billing - FACTURACIÓN";
        Console.ForegroundColor = ConsoleColor.Yellow;

        var endpointConfiguration = new EndpointConfiguration("Billing");
        endpointConfiguration.UseSerialization<NewtonsoftJsonSerializer>();

        var transport = endpointConfiguration.UseTransport<LearningTransport>();
        transport.StorageDirectory(@"c:\LabNServiceBus_Data");

        endpointConfiguration.UsePersistence<LearningPersistence>();

        var endpointInstance = await Endpoint.Start(endpointConfiguration);

        Console.WriteLine("Billing listo para cobrar...");
        Console.ReadLine();
        await endpointInstance.Stop();
    }
}