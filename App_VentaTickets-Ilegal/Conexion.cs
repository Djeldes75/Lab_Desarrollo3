using System;
using System.Configuration;
using System.Data.SqlClient;

namespace ConexionDB
{
    public class ConexionBD
    {
        public static SqlConnection ObtenerConexion()
        {
            string cadena = ConfigurationManager.ConnectionStrings["Mi_HermosaDB_Para_Actividades_Ilegales"].ConnectionString;

            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();
            return cn;
        }
    }
}
