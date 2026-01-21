using NServiceBus;
using Sales.Messages;
using System;
using System.Data;
using Microsoft.Data.SqlClient; 
using System.Threading.Tasks;

namespace Sales
{
    public class PlaceOrderHandler : IHandleMessages<PlaceOrder>
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\VS_Projects\DS3\Laboratorio\SistemaVentas\Sales\VentasDB.mdf;Integrated Security=True";

        public Task Handle(PlaceOrder message, IMessageHandlerContext context)
        {
            Console.WriteLine($"Recibida orden de: {message.Nombres} {message.Apellidos}");

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_InsertarOrden", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@OrderId", message.OrderId);
                        cmd.Parameters.AddWithValue("@Descripcion", message.Descripcion);
                        cmd.Parameters.AddWithValue("@Precio", message.Precio);
                        cmd.Parameters.AddWithValue("@FechaIngreso", message.FechaIngreso);
                        cmd.Parameters.AddWithValue("@TipoDocumento", message.TipoDocumento);
                        cmd.Parameters.AddWithValue("@Documento", message.Documento);
                        cmd.Parameters.AddWithValue("@Nombres", message.Nombres);
                        cmd.Parameters.AddWithValue("@Apellidos", message.Apellidos);

                        cmd.ExecuteNonQuery();
                    }
                }
                Console.WriteLine("--> ¡Guardado en Base de Datos exitosamente!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }

            return Task.CompletedTask;
        }
    }
}