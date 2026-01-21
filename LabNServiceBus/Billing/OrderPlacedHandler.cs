using NServiceBus;
using Sales.Messages;
using System;
using System.Threading.Tasks;

public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
{
    public Task Handle(OrderPlaced message, IMessageHandlerContext context)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("-------------------------------------");
        Console.WriteLine($"[$$] FACTURANDO ORDEN: {message.OrderId}");
        Console.WriteLine($"     Cliente: {message.Nombres} {message.Apellidos}");
        Console.WriteLine($"     Total:   ${message.Precio}");
        Console.WriteLine("-------------------------------------");
        return Task.CompletedTask;
    }
}