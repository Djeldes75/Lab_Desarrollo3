using Microsoft.IdentityModel.Protocols;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Noticias_registro.app
{
    class Program
    {
        static void Main(string[] args)
        {
            {

                //cmd.commandtype = System.Data.CommandType.StoredProcedure;

                string connectionString =
                    "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\Dominique Jeldes\\Desktop\\DS3 (T-L)\\Laboratorio\\AppPara telenoticias\\MyDB.mdf\";Integrated Security=True";
                
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                while (true)
                {
                    Console.WriteLine("=== Registro de Noticias ===");

                    Console.Write("Código: ");
                    string codigo = Console.ReadLine();

                    Console.Write("Titular: ");
                    string titular = Console.ReadLine();

                    Console.Write("Lead: ");
                    string lead = Console.ReadLine();

                    Console.Write("Texto Completo: ");
                    string texto = Console.ReadLine();

                    Console.Write("Imagen: ");
                    string imagen = Console.ReadLine();

                    Console.Write("Autor: ");
                    string autor = Console.ReadLine();

                    Console.Write("Fecha de la noticia (yyyy-MM-dd): ");
                    string fechaNoticia = Console.ReadLine();

                    Console.Write("Estado (número): ");
                    string estado = Console.ReadLine();

                    Console.Write("Sección: ");
                    string seccion = Console.ReadLine();

                    Console.Write("Periódico: ");
                    string periodico = Console.ReadLine();

                    //cmd.CommandText - "ppGetClientes'

                    //SqlDataReader dr = cmd.ExecuteReader();

                    //agregar parametros
                    //cmd.Parameters.AddValue("@nombre", nombre);
                    //cmd/commandtext
                    string sql =
                        "INSERT INTO tblNoticias " +
                        "(codigo, titular, lead, textocompleto, imagen, autor, fechanoticia, estado, seccion, periodico) VALUES (" +
                        "'" + codigo + "', " +
                        "'" + titular + "', " +
                        "'" + lead + "', " +
                        "'" + texto + "', " +
                        "'" + imagen + "', " +
                        "'" + autor + "', " +
                        "'" + fechaNoticia + "', " +
                        estado + ", " +
                        "'" + seccion + "', " +
                        "'" + periodico + "'" +
                        ");";

                    //para que no explote:
                    //cmd.commandtype = system.data.commandtype.storedprocedure;

                    /*
                     crear las stored procedure para esta apps
                     */

                    SqlCommand cmd = new SqlCommand(sql, connection);
                    cmd.ExecuteNonQuery();

                    Console.WriteLine("\nNoticia guardada correctamente.\n");

                    Console.Write("¿Desea registrar otra noticia? (S/N): ");
                    string opcion = Console.ReadLine();

                    if (opcion.ToUpper() == "N")
                    {
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    }

                    Console.Clear();
                }

                connection.Close();
            }
        }
    }

}
