using PizzaraWebService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PizzaraWebService.Data
{
    public class PizarraRepository
    {
        private readonly string _conn = ConfigurationManager.ConnectionStrings["PizarraDB"].ConnectionString;

        public (string Resultado, object InsertedId) InsertEvento(EventoDto ev)
        {
            using (var conn = new SqlConnection(_conn))
            using (var cmd = new SqlCommand("SP_InsertEvento", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idjuego", ev.IdJuego);
                cmd.Parameters.AddWithValue("@Carrera", ev.Carrera);
                cmd.Parameters.AddWithValue("@Inning", ev.Inning);
                cmd.Parameters.AddWithValue("@Abre", (object)ev.Abre ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Cierra", (object)ev.Cierra ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Pelotero", (object)ev.Pelotero ?? DBNull.Value);

                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return (rdr["Resultado"].ToString(), rdr["InsertedId"] == DBNull.Value ? null : rdr["InsertedId"]);
                    }
                }
            }
            return ("ERROR", null);
        }

        public string UpsertJuego(string idJuego, string abre, string cierra, int? carreraAbre, int? carreraCierra, int? inning, int? outs)
        {
            using (var conn = new SqlConnection(_conn))
            using (var cmd = new SqlCommand("SP_UpsertJuego", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdJuego", idJuego);
                cmd.Parameters.AddWithValue("@Abre", (object)abre ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Cierra", (object)cierra ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CarreraAbre", (object)carreraAbre ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CarreraCierra", (object)carreraCierra ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Inning", (object)inning ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CantidadOuts", (object)outs ?? DBNull.Value);

                conn.Open();
                using (var rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        return rdr[0].ToString();
                    }
                }
            }
            return "ERROR";
        }
    }

}