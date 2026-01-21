using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ServidorRegistroCliente.Models;

namespace ServidorRegistroCliente.DataAccess
{
    public class ClienteRepository
    {
        private readonly string _connectionString;

        public ClienteRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["DBConnection"].ConnectionString;
        }

        public string InsertarCliente(Cliente cliente)
        {
            using (SqlConnection cn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand("sp_InsertCliente", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@tipodocumento", cliente.TipoDocumento);
                cmd.Parameters.AddWithValue("@documento", cliente.Documento);
                cmd.Parameters.AddWithValue("@nombres", cliente.Nombres);
                cmd.Parameters.AddWithValue("@apellidos", cliente.Apellidos);
                cmd.Parameters.AddWithValue("@sexo", cliente.Sexo);
                cmd.Parameters.AddWithValue("@fechanacimiento", cliente.FechaNacimiento);
                cmd.Parameters.AddWithValue("@correo", cliente.Correo);
                cmd.Parameters.AddWithValue("@telefono", cliente.Telefono);

                cn.Open();
                var result = cmd.ExecuteScalar();

                return result?.ToString() ?? "ERROR_INTERNO";
            }
        }
    }
}
