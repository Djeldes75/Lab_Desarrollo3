using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ReclutarDominicanos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = ConfigurationManager
                .ConnectionStrings["CnSql"].ConnectionString;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== GESTIÓN DE CANDIDATOS ===");
                Console.WriteLine("1. Registrar candidato (insertar si no existe)");
                Console.WriteLine("2. Buscar candidato por documento");
                Console.WriteLine("0. Salir");
                Console.Write("Opción: ");

                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        RegistrarCandidato(connectionString);
                        break;
                    case "2":
                        BuscarCandidato(connectionString);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Opción inválida.");
                        Pausa();
                        break;
                }
            }
        }

        static void RegistrarCandidato(string connectionString)
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRAR CANDIDATO ===");

            Console.Write("Tipo de documento (int): ");
            int tipoDoc = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Documento: ");
            string documento = Console.ReadLine();

            Console.Write("Nombres: ");
            string nombres = Console.ReadLine();

            Console.Write("Apellidos: ");
            string apellidos = Console.ReadLine();

            Console.Write("Fecha de nacimiento (yyyy-mm-dd): ");
            DateTime fechaNac = DateTime.Parse(Console.ReadLine() ?? DateTime.Now.ToString("yyyy-MM-dd"));

            Console.Write("Peso (kg): ");
            decimal peso = decimal.Parse(Console.ReadLine() ?? "0");

            Console.Write("Estatura (m): ");
            decimal estatura = decimal.Parse(Console.ReadLine() ?? "0");

            Console.Write("Cantidad de hijos: ");
            int cantHijos = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Condición física: ");
            string condFisica = Console.ReadLine();

            Console.Write("Estado (int): ");
            int estado = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Formación académica: ");
            string formAcad = Console.ReadLine();

            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("spCandidatos_InsertarSiNoExiste", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@TipoDocumento", tipoDoc);
                cmd.Parameters.AddWithValue("@Documento", documento);
                cmd.Parameters.AddWithValue("@Nombres", nombres);
                cmd.Parameters.AddWithValue("@Apellidos", apellidos);
                cmd.Parameters.AddWithValue("@FechaNac", fechaNac);
                cmd.Parameters.AddWithValue("@Peso", peso);
                cmd.Parameters.AddWithValue("@Estatura", estatura);
                cmd.Parameters.AddWithValue("@CantHijos", cantHijos);
                cmd.Parameters.AddWithValue("@CondFisica", condFisica);
                cmd.Parameters.AddWithValue("@Estado", estado);
                cmd.Parameters.AddWithValue("@FormAcad", formAcad);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Console.WriteLine();
                        Console.WriteLine("=== DATOS DEL CANDIDATO ===");
                        MostrarCandidatoDesdeReader(dr);
                    }
                    else
                    {
                        Console.WriteLine("No se devolvieron datos.");
                    }
                }
            }

            Pausa();
        }

        static void BuscarCandidato(string connectionString)
        {
            Console.Clear();
            Console.WriteLine("=== BUSCAR CANDIDATO ===");
            Console.Write("Documento: ");
            string documento = Console.ReadLine();

            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand("spCandidatos_ObtenerPorDocumento", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Documento", documento);

                cn.Open();

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        Console.WriteLine();
                        Console.WriteLine("=== DATOS DEL CANDIDATO ===");
                        MostrarCandidatoDesdeReader(dr);
                    }
                    else
                    {
                        Console.WriteLine("No se encontró un candidato con ese documento.");
                    }
                }
            }

            Pausa();
        }

        static void MostrarCandidatoDesdeReader(SqlDataReader dr)
        {
            Console.WriteLine($"Id: {dr["Id"]}");
            Console.WriteLine($"TipoDocumento: {dr["TipoDocumento"]}");
            Console.WriteLine($"Documento: {dr["Documento"]}");
            Console.WriteLine($"Nombres: {dr["Nombres"]}");
            Console.WriteLine($"Apellidos: {dr["Apellidos"]}");
            Console.WriteLine($"FechaNac: {Convert.ToDateTime(dr["FechaNac"]).ToShortDateString()}");
            Console.WriteLine($"FechaIng: {Convert.ToDateTime(dr["FechaIng"]).ToShortDateString()}");
            Console.WriteLine($"Peso: {dr["Peso"]}");
            Console.WriteLine($"Estatura: {dr["Estatura"]}");
            Console.WriteLine($"CantHijos: {dr["CantHijos"]}");
            Console.WriteLine($"CondFisica: {dr["CondFisica"]}");
            Console.WriteLine($"Estado: {dr["Estado"]}");
            Console.WriteLine($"FormAcad: {dr["FormAcad"]}");
        }

        static void Pausa()
        {
            Console.WriteLine();
            Console.WriteLine("Presiona una tecla para continuar...");
            Console.ReadKey();
        }
    }
}