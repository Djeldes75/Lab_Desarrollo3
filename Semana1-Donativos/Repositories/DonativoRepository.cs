using System;
using System.Data;
using MySql.Data.MySqlClient;
using Semana1_Donativos.Data;
using Semana1_Donativos.Models;

namespace Semana1_Donativos.Repositories
{
    public class DonativoRepository
    {
        public int Insert(Donativo d)
        {
            const string sql = @"
                INSERT INTO Donativos
                (Operativo, Pais, Lote, Descripcion, Cantidad, Fecha_Ingreso, Estado)
                VALUES (@operativo, @pais, @lote, @descripcion, @cantidad, @fecha, @estado);
                SELECT LAST_INSERT_ID();";

            using (MySqlConnection cn = Db.GetOpenConnection())
            using (MySqlCommand cmd = new MySqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@operativo", d.Operativo);
                cmd.Parameters.AddWithValue("@pais", d.Pais);
                cmd.Parameters.AddWithValue("@lote", d.Lote);
                cmd.Parameters.AddWithValue("@descripcion", d.Descripcion);
                cmd.Parameters.AddWithValue("@cantidad", d.Cantidad);
                cmd.Parameters.AddWithValue("@fecha", d.Fecha_Ingreso.Date);
                cmd.Parameters.AddWithValue("@estado", d.Estado);

                object result = cmd.ExecuteScalar();
                return Convert.ToInt32(result);
            }
        }

        public int Delete(int id)
        {
            const string sql = @"DELETE FROM Donativos WHERE ID=@id;";
            using (var cn = Db.GetOpenConnection())
            using (var cmd = new MySqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteNonQuery();
            }
        }

        public int Update(Donativo d)
        {
            const string sql = @"
        UPDATE Donativos
        SET Operativo=@operativo, 
            Pais=@pais, 
            Lote=@lote, 
            Descripcion=@descripcion, 
            Cantidad=@cantidad, 
            Fecha_Ingreso=@fecha, 
            Estado=@estado
        WHERE ID=@id;";
            using (var cn = Db.GetOpenConnection())
            using (var cmd = new MySqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@operativo", d.Operativo);
                cmd.Parameters.AddWithValue("@pais", d.Pais);
                cmd.Parameters.AddWithValue("@lote", d.Lote);
                cmd.Parameters.AddWithValue("@descripcion", d.Descripcion);
                cmd.Parameters.AddWithValue("@cantidad", d.Cantidad);
                cmd.Parameters.AddWithValue("@fecha", d.Fecha_Ingreso.Date);
                cmd.Parameters.AddWithValue("@estado", d.Estado);
                cmd.Parameters.AddWithValue("@id", d.ID);
                return cmd.ExecuteNonQuery();
            }
        }
        public bool Exists(Donativo d)
        {
            const string sql = @"
        SELECT 1
        FROM Donativos
        WHERE Operativo=@operativo AND Pais=@pais AND Lote=@lote AND Fecha_Ingreso=@fecha
        LIMIT 1;";

            using (var cn = Db.GetOpenConnection())
            using (var cmd = new MySqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@operativo", d.Operativo);
                cmd.Parameters.AddWithValue("@pais", d.Pais);
                cmd.Parameters.AddWithValue("@lote", d.Lote);
                cmd.Parameters.AddWithValue("@fecha", d.Fecha_Ingreso.Date);
                var o = cmd.ExecuteScalar();
                return o != null;
            }
        }
        public bool ExistsForUpdate(int id, Donativo d)
        {
            const string sql = @"
        SELECT 1
        FROM Donativos
        WHERE Operativo=@operativo AND Pais=@pais AND Lote=@lote AND Fecha_Ingreso=@fecha
          AND ID <> @id
        LIMIT 1;";

            using (var cn = Db.GetOpenConnection())
            using (var cmd = new MySqlCommand(sql, cn))
            {
                cmd.Parameters.AddWithValue("@operativo", d.Operativo);
                cmd.Parameters.AddWithValue("@pais", d.Pais);
                cmd.Parameters.AddWithValue("@lote", d.Lote);
                cmd.Parameters.AddWithValue("@fecha", d.Fecha_Ingreso.Date);
                cmd.Parameters.AddWithValue("@id", id);
                var o = cmd.ExecuteScalar();
                return o != null;
            }
        }

        public DataTable GetAllAsDataTable()
        {
            const string sql = @"
                SELECT ID, 
                    Operativo, 
                    Pais, 
                    Lote, 
                    Descripcion, 
                    Cantidad, 
                    Fecha_Ingreso, 
                    Estado
                FROM Donativos
                ORDER BY ID ASC;";

            DataTable dt = new DataTable();
            using (MySqlConnection cn = Db.GetOpenConnection())
            using (MySqlDataAdapter da = new MySqlDataAdapter(sql, cn))
            {
                da.Fill(dt);
            }
            return dt;
        }
    }
}
