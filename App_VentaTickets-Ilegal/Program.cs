using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ingresar_Clientes;
using Ingresar_Tickets;

namespace App_VentaTickets_Ilegal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int opcion = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("Te vendemos Tickts 100% Legal *wink wink*\n");
                Console.WriteLine("1. Registrar Cliente");
                Console.WriteLine("2. Buscar Cliente");
                Console.WriteLine("3. Registrar Ticket");
                Console.WriteLine("4. Buscar Tickets por Documento");
                Console.WriteLine("5. Salir");
                Console.Write("\nSeleccione una opción: ");
                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        RegistrarCliente();
                        break;

                    case 2:
                        BuscarCliente();
                        break;

                    case 3:
                        RegistrarTicket();
                        break;

                    case 4:
                        BuscarTickets();
                        break;
                }

                Console.WriteLine("\nPresione una tecla para continuar...");
                Console.ReadKey();

            } while (opcion != 5);
        }

        static void RegistrarCliente()
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

            Console.Write("Sexo (M/F): ");
            string sexo = Console.ReadLine();

            Console.Write("Cantidad de boletos: ");
            int cantB = int.Parse(Console.ReadLine());

            Console.Write("Estado: ");
            string estado = Console.ReadLine();

            string r = Cliente.InsertarCliente(
                tipoDoc, documento, nombres, apellidos,
                fechaNac, sexo, cantB, estado);

            Console.WriteLine("Cliente insertado correctamente.");
        }


        static void BuscarCliente()
        {
            Console.Write("Documento: ");
            string doc = Console.ReadLine();

            Cliente.BuscarCliente(doc);
        }

        static void RegistrarTicket()
        {
            Console.Write("Tipo documento: ");
            int tipoDoc = int.Parse(Console.ReadLine());

            Console.Write("Documento: ");
            string documento = Console.ReadLine();

            Console.Write("Nombre comprador: ");
            string nombre = Console.ReadLine();

            Console.Write("Concierto: ");
            string concierto = Console.ReadLine();

            Console.Write("Venue: ");
            string venue = Console.ReadLine();

            Console.Write("Fecha concierto (yyyy-mm-dd): ");
            DateTime fecha = DateTime.Parse(Console.ReadLine());

            Console.Write("Nota: ");
            string nota = Console.ReadLine();

            Console.Write("Seguridad: ");
            string seg = Console.ReadLine();

            Console.Write("Estado: ");
            int estado = int.Parse(Console.ReadLine());

            string r = Ticket.InsertarTicket(
                tipoDoc, documento, nombre, concierto, venue,
                fecha, nota, seg, estado);

            if (r == "NO_CLIENTE")
                Console.WriteLine("No existe cliente con ese documento.");
            else
                Console.WriteLine("Ticket registrado correctamente.");
        }

        static void BuscarTickets()
        {
            Console.Write("Documento: ");
            string doc = Console.ReadLine();

            Ticket.BuscarTickets(doc);
        }
    }
}