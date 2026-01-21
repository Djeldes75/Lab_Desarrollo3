using App_ControlPresos;
using System;
using System.Data;
using System.Data.SqlClient;

namespace App_ControlPresos
{
    public class Evento
    {
        public static void BuscarEventos(string documento)
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT * FROM tblEventos WHERE Documento = @Documento", cn);

                cmd.Parameters.AddWithValue("@Documento", documento);
                SqlDataReader dr = cmd.ExecuteReader();

                if (!dr.HasRows)
                {
                    Console.WriteLine("El preso no tiene eventos.");
                    return;
                }

                while (dr.Read())
                {
                    Console.WriteLine("\n--- EVENTO ---");
                    Console.WriteLine("ID: " + dr["Id"]);
                    Console.WriteLine("Descripción: " + dr["Descripcion"]);
                    Console.WriteLine("Estado: " + dr["Estado"]);
                    Console.WriteLine("Fecha: " + dr["FechaIngreso"]);
                    Console.WriteLine("Digitado por: " + dr["DigitadoPor"]);
                    Console.WriteLine("Localidad: " + dr["Localidad"]);
                }
            }
        }
    }
}
