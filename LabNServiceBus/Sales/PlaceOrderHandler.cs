using NServiceBus;
using Sales.Messages;
using System;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
{
    public async Task Handle(PlaceOrder message, IMessageHandlerContext context)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"\n[⬇] Recibida Orden: {message.OrderId}");

        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\VS_Projects\DS3\Laboratorio\LabNServiceBus\Sales\SalesDB.mdf;Integrated Security=True";

        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            await conn.OpenAsync(context.CancellationToken);
            string query = "INSERT INTO Orders (OrderId, Descripcion, Precio, FechaIngreso, TipoDocumento, Documento, Nombres, Apellidos) VALUES (@oid, @desc, @prec, @fecha, @tipo, @doc, @nom, @ape)";

            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@oid", message.OrderId);
                cmd.Parameters.AddWithValue("@desc", message.Descripcion);
                cmd.Parameters.AddWithValue("@prec", message.Precio);
                cmd.Parameters.AddWithValue("@fecha", message.FechaIngreso);
                cmd.Parameters.AddWithValue("@tipo", message.TipoDocumento);
                cmd.Parameters.AddWithValue("@doc", message.Documento);
                cmd.Parameters.AddWithValue("@nom", message.Nombres);
                cmd.Parameters.AddWithValue("@ape", message.Apellidos);

                await cmd.ExecuteNonQueryAsync(context.CancellationToken);
            }
        }
        Console.WriteLine("[💾] Guardado en Base de Datos SQL.");

        var eventMessage = new OrderPlaced
        {
            OrderId = message.OrderId,
            Descripcion = message.Descripcion,
            Precio = message.Precio,
            FechaIngreso = message.FechaIngreso,
            TipoDocumento = message.TipoDocumento,
            Documento = message.Documento,
            Nombres = message.Nombres,
            Apellidos = message.Apellidos
        };

        await context.Publish(eventMessage);
        Console.WriteLine("[📢] Evento 'OrderPlaced' publicado.\n");
    }
}