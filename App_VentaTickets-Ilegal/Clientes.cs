using App_VentaTickets_Ilegal;
using ConexionDB;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Ingresar_Clientes
{
    public class Cliente
    {
        public static string InsertarCliente(
            int tipoDoc,
            string documento,
            string nombres,
            string apellidos,
            DateTime fechaNac,
            string sexo,
            int cantidadBoletos,
            string estado)
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_UpsertCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TipoDocumento", tipoDoc);
                cmd.Parameters.AddWithValue("@Documento", documento);
                cmd.Parameters.AddWithValue("@Nombres", nombres);
                cmd.Parameters.AddWithValue("@Apellidos", apellidos);
                cmd.Parameters.AddWithValue("@FechaNac", fechaNac);
                cmd.Parameters.AddWithValue("@Sexo", sexo);
                cmd.Parameters.AddWithValue("@CantidadBoletos", cantidadBoletos);
                cmd.Parameters.AddWithValue("@Estado", estado);

                cmd.ExecuteNonQuery();
                return "OK";
            }
        }

        public static void BuscarCliente(string documento)
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_GetCliente", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Documento", documento);

                SqlDataReader dr = cmd.ExecuteReader();

                if (!dr.HasRows)
                {
                    Console.WriteLine("No existe el cliente.");
                    return;
                }

                while (dr.Read())
                {
                    Console.WriteLine("\n=== CLIENTE ===");
                    Console.WriteLine("Id: " + dr["Id"]);
                    Console.WriteLine("Nombres: " + dr["Nombres"]);
                    Console.WriteLine("Apellidos: " + dr["Apellidos"]);
                    Console.WriteLine("Documento: " + dr["Documento"]);
                    Console.WriteLine("Sexo: " + dr["Sexo"]);
                    Console.WriteLine("Cantidad Boletos: " + dr["CantidadBoletos"]);
                    Console.WriteLine("Estado: " + dr["Estado"]);
                }
            }
        }
    }
}
