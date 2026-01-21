using NServiceBus;
using Sales.Messages;
using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

public class OrderPlacedHandler : IHandleMessages<OrderPlaced>
{
    public Task Handle(OrderPlaced message, IMessageHandlerContext context)
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine($"[🚚] Iniciando envío para: {message.OrderId}");

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\VS_Projects\DS3\Laboratorio\LabNServiceBus\Shipping\ShippingDB.mdf;Integrated Security=True";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            using (SqlCommand cmd = new SqlCommand("InsertShippingLog", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderId", message.OrderId);
                cmd.Parameters.AddWithValue("@Estado", "En Camino");
                cmd.Parameters.AddWithValue("@FechaRegistro", DateTime.Now);

                cmd.ExecuteNonQuery();
            }
        }
        Console.WriteLine("[✅] Log de envío guardado (SP ejecutado).");
        return Task.CompletedTask;
    }
}