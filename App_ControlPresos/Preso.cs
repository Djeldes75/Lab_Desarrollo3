using App_ControlPresos;
using System;
using System.Data;
using System.Data.SqlClient;

namespace App_ControlPresos
{
    public class Preso
    {
        public static string RegistrarPresoConEvento(
            int tipoDoc,
            string documento,
            string nombres,
            string apellidos,
            DateTime fechaNac,
            string pena,
            string motivo,
            string descripcionPreso,
            string tipo,
            string estadoPreso,
            string localidadPreso,
            string descripcionEvento,
            string estadoEvento,
            string digitadoPor,
            string localidadEvento
        )
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarEventoPreso", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TipoDocumento", tipoDoc);
                cmd.Parameters.AddWithValue("@Documento", documento);
                cmd.Parameters.AddWithValue("@Nombres", nombres);
                cmd.Parameters.AddWithValue("@Apellidos", apellidos);
                cmd.Parameters.AddWithValue("@FechaNacimiento", fechaNac);
                cmd.Parameters.AddWithValue("@Pena", pena);
                cmd.Parameters.AddWithValue("@Motivo", motivo);
                cmd.Parameters.AddWithValue("@DescripcionPreso", descripcionPreso);
                cmd.Parameters.AddWithValue("@Tipo", tipo);
                cmd.Parameters.AddWithValue("@EstadoPreso", estadoPreso);
                cmd.Parameters.AddWithValue("@LocalidadPreso", localidadPreso);

                // Parte del evento
                cmd.Parameters.AddWithValue("@DescripcionEvento", descripcionEvento);
                cmd.Parameters.AddWithValue("@EstadoEvento", estadoEvento);
                cmd.Parameters.AddWithValue("@DigitadoPor", digitadoPor);
                cmd.Parameters.AddWithValue("@LocalidadEvento", localidadEvento);

                cmd.ExecuteNonQuery();
                return "OK";
            }
        }

        public static void BuscarPreso(string documento)
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM tblPresos WHERE Documento = @Documento", cn);

                cmd.Parameters.AddWithValue("@Documento", documento);
                SqlDataReader dr = cmd.ExecuteReader();

                if (!dr.HasRows)
                {
                    Console.WriteLine("No existe un preso con ese documento.");
                    return;
                }

                while (dr.Read())
                {
                    Console.WriteLine("\n=== PRESO ===");
                    Console.WriteLine("ID: " + dr["Id"]);
                    Console.WriteLine("Nombre: " + dr["Nombres"]);
                    Console.WriteLine("Apellidos: " + dr["Apellidos"]);
                    Console.WriteLine("Pena: " + dr["Pena"]);
                    Console.WriteLine("Motivo: " + dr["Motivo"]);
                    Console.WriteLine("Estado: " + dr["Estado"]);
                    Console.WriteLine("Eventos: " + dr["CantidadEventos"]);
                    Console.WriteLine("Último Evento: " + dr["FechaUltimoEvento"]);
                }
            }
        }
    }
}
