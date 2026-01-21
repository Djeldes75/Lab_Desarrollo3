using App_ControlPresos;
using System;
using System.Data.SqlClient;

namespace App_ControlPresos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int opcion = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("=== SISTEMA DE CONTROL DE PRESOS ===\n");
                Console.WriteLine("1. Registrar preso y evento");
                Console.WriteLine("2. Buscar preso");
                Console.WriteLine("3. Buscar eventos de un preso");
                Console.WriteLine("4. Registrar pago de nómina");
                Console.WriteLine("5. Mostrar todas las nóminas");
                Console.WriteLine("6. Salir");
                Console.Write("\nSeleccione una opción: ");

                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        RegistrarPresoYEvento();
                        break;

                    case 2:
                        BuscarPreso();
                        break;

                    case 3:
                        BuscarEventos();
                        break;

                    case 4:
                        RegistrarPagoNomina();
                        break;

                    case 5:
                        MostrarNominas();
                        break;

                    case 6:
                        break;

                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }

                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();

            } while (opcion != 6);
        }

        static void RegistrarPresoYEvento()
        {
            Console.Write("Tipo documento: ");
            int tipoDoc = int.Parse(Console.ReadLine());

            Console.Write("Documento: ");
            string documento = Console.ReadLine();

            Console.Write("Nombres: ");
            string nombres = Console.ReadLine();

            Console.Write("Apellidos: ");
            string apellidos = Console.ReadLine();

            Console.Write("Fecha nacimiento (yyyy-mm-dd): ");
            DateTime fechaNac = DateTime.Parse(Console.ReadLine());

            Console.Write("Pena: ");
            string pena = Console.ReadLine();

            Console.Write("Motivo: ");
            string motivo = Console.ReadLine();

            Console.Write("Descripción preso: ");
            string descPreso = Console.ReadLine();

            Console.Write("Tipo: ");
            string tipo = Console.ReadLine();

            Console.Write("Estado preso: ");
            string estadoPreso = Console.ReadLine();

            Console.Write("Localidad preso: ");
            string locPreso = Console.ReadLine();

            // Evento
            Console.Write("Descripción del evento: ");
            string descEvento = Console.ReadLine();

            Console.Write("Estado evento: ");
            string estadoEvento = Console.ReadLine();

            Console.Write("Digitado por: ");
            string digitado = Console.ReadLine();

            Console.Write("Localidad evento: ");
            string locEvento = Console.ReadLine();

            string r = Preso.RegistrarPresoConEvento(
                tipoDoc, documento, nombres, apellidos, fechaNac,
                pena, motivo, descPreso, tipo, estadoPreso, locPreso,
                descEvento, estadoEvento, digitado, locEvento);

            Console.WriteLine(r == "OK"
                ? "Transacción completada correctamente."
                : "Error al registrar.");
        }

        static void BuscarPreso()
        {
            Console.Write("Documento: ");
            string doc = Console.ReadLine();

            Preso.BuscarPreso(doc);
        }


        static void BuscarEventos()
        {
            Console.Write("Documento: ");
            string doc = Console.ReadLine();

            Evento.BuscarEventos(doc);
        }

        static void RegistrarPagoNomina()
        {
            Console.Write("Tipo documento: ");
            int tipoDoc = int.Parse(Console.ReadLine());

            Console.Write("Documento: ");
            string documento = Console.ReadLine();

            Console.Write("Año: ");
            int año = int.Parse(Console.ReadLine());

            Console.Write("Mes: ");
            int mes = int.Parse(Console.ReadLine());

            Console.Write("Quincena (1 o 2, 0 si salario 13): ");
            int quincena = int.Parse(Console.ReadLine());

            Console.Write("Tipo de pago (Normal / Salario13): ");
            string tipoPago = Console.ReadLine();

            Pago.RegistrarPago(tipoDoc, documento, año, mes, quincena, tipoPago);
        }

        static void MostrarNominas()
        {
            using (SqlConnection cn = ConexionBD.ObtenerConexion())
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT TipoDocumento, Documento, TipoPago, Monto, FechaIngreso, Estado, Año, Mes, Quincena FROM tblPagos",
                    cn);

                SqlDataReader dr = cmd.ExecuteReader();

                Console.WriteLine("\n===== LISTADO DE NÓMINAS REGISTRADAS =====\n");

                while (dr.Read())
                {
                    Console.WriteLine($"Documento: {dr["Documento"]}");
                    Console.WriteLine($"Tipo Pago: {dr["TipoPago"]}");
                    Console.WriteLine($"Monto: {dr["Monto"]}");
                    Console.WriteLine($"Fecha: {dr["FechaIngreso"]}");
                    Console.WriteLine($"Año: {dr["Año"]}  Mes: {dr["Mes"]}  Quincena: {dr["Quincena"]}");
                    Console.WriteLine($"Estado: {dr["Estado"]}");
                    Console.WriteLine("---------------------------------------------");
                }

                dr.Close();
            }
        }

    }
}
