using System.Diagnostics;
using MySql.Data.MySqlClient;

namespace Semana1_Donativos.Data
{
    public static class Db
    {
        public static MySqlConnection GetOpenConnection()
        {
            var csb = new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Port = 3306,
                Database = "semana1",
                UserID = "root",
                Password = "vainilla",
                Pooling = true,
                // SslMode = MySqlSslMode.None

            };

            Debug.WriteLine("[ConnStr] " + csb.ConnectionString);
            var cn = new MySqlConnection(csb.ConnectionString);
            cn.Open();
            return cn;
        }
    }
}
