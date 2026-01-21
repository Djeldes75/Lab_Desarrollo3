using System.Data.SqlClient;
using System.Configuration;

namespace App_ControlPresos
{
    public class ConexionBD
    {
        public static SqlConnection ObtenerConexion()
        {
            string cadena = ConfigurationManager
                .ConnectionStrings["DB_Presos"].ConnectionString;

            SqlConnection cn = new SqlConnection(cadena);
            cn.Open();
            return cn;
        }
    }
}
