using App_VentaTickets_Ilegal;
using ConexionDB;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Ingresar_Tickets
{
    public class Ticket
    {
        public static string InsertarTicket(
            int tipoDoc,
            string documento,
            string nombre,
            string concierto,
            string venue,
            DateTime fecha,
            string nota,
            string seguridad,
            int estado)
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_UpsertTicket", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TipoDocumento", tipoDoc);
                cmd.Parameters.AddWithValue("@Documento", documento);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Concierto", concierto);
                cmd.Parameters.AddWithValue("@Venue", venue);
                cmd.Parameters.AddWithValue("@Fecha", fecha);
                cmd.Parameters.AddWithValue("@Nota", nota);
                cmd.Parameters.AddWithValue("@Seguridad", seguridad);
                cmd.Parameters.AddWithValue("@Estado", estado);

                cmd.ExecuteNonQuery();
                return "OK";
            }
        }

        public static void BuscarTickets(string documento)
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_GetTickets", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Documento", documento);

                SqlDataReader dr = cmd.ExecuteReader();

                if (!dr.HasRows)
                {
                    Console.WriteLine("El cliente no tiene tickets registrados.");
                    return;
                }

                while (dr.Read())
                {
                    Console.WriteLine("\n--- TICKET REGISTRADO ---");
                    Console.WriteLine("ID:        " + dr["Id"]);
                    Console.WriteLine("Nombre:    " + dr["Nombre"]);
                    Console.WriteLine("Concierto: " + dr["Concierto"]);
                    Console.WriteLine("Venue:     " + dr["Venue"]);
                    Console.WriteLine("Fecha:     " + dr["Fecha"]);
                    Console.WriteLine("Nota:      " + dr["Nota"]);
                    Console.WriteLine("Seguridad: " + dr["Seguridad"]);
                    Console.WriteLine("Estado:    " + dr["Estado"]);
                }
            }
        }
    }
}
