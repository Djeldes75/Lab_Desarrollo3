using NServiceBus;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        Console.Title = "Shipping - ENVÍOS";
        Console.ForegroundColor = ConsoleColor.Magenta;

        var endpointConfiguration = new EndpointConfiguration("Shipping");
        endpointConfiguration.UseSerialization<NewtonsoftJsonSerializer>();

        var transport = endpointConfiguration.UseTransport<LearningTransport>();
        transport.StorageDirectory(@"c:\LabNServiceBus_Data");

        endpointConfiguration.UsePersistence<LearningPersistence>();

        var endpointInstance = await Endpoint.Start(endpointConfiguration);

        Console.WriteLine("Shipping listo para despachar...");
        Console.ReadLine();
        await endpointInstance.Stop();
    }
}