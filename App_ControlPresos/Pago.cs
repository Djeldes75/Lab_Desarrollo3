using System;
using System.Data.SqlClient;

namespace App_ControlPresos
{
    public class Pago
    {
        public static void RegistrarPago(
            int tipoDoc,
            string documento,
            int año,
            int mes,
            int quincena,
            string tipoPago)
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand("sp_InsertPagoNomina", cn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TipoDocumento", tipoDoc);
                cmd.Parameters.AddWithValue("@Documento", documento.Trim());
                cmd.Parameters.AddWithValue("@Año", año);
                cmd.Parameters.AddWithValue("@Mes", mes);
                cmd.Parameters.AddWithValue("@Quincena", quincena);
                cmd.Parameters.AddWithValue("@TipoPago", tipoPago);

                cmd.ExecuteNonQuery();

                Console.WriteLine("\n>>> Pago registrado correctamente.\n");
            }
        }
    }
}
