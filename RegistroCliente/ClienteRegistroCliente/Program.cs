using System;
using ClienteRegistroCliente.Models;
using ClienteRegistroCliente.Services;

namespace ClienteRegistroCliente
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "CLIENTE – Registro de Clientes";
            PrintHeader();

            var clientService = new TcpClientService("127.0.0.1");

            var cliente = new Cliente
            {
                TipoDocumento = 1,
                Documento = "77077813235",
                Nombres = "Hornet",
                Apellidos = "Errah",
                Sexo = "F",
                FechaNacimiento = new DateTime(2025, 1, 1),
                Correo = "Silksong@gmail.com",
                Telefono = "+1-828-335-0947"
            };

            LogInfo("Preparando registro del cliente...");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n=== JSON A ENVIAR ===");
            Console.ResetColor();
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(cliente, Newtonsoft.Json.Formatting.Indented));
            Console.WriteLine();

            LogInfo("Enviando datos al servidor...");

            string respuesta = clientService.EnviarCliente(cliente);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n[OK] Respuesta del servidor: {respuesta}");
            Console.ResetColor();

            LogInfo("Proceso completado.");
            Console.WriteLine("\nPresione cualquier tecla para salir...");
            Console.ReadKey();
        }

        private static void PrintHeader()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("==============================================");
            Console.WriteLine("        CLIENTE – REGISTRO DE CLIENTES");
            Console.WriteLine("==============================================");
            Console.ResetColor();
        }

        private static void LogInfo(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"[INFO {DateTime.Now:HH:mm:ss}] ");
            Console.ResetColor();
            Console.WriteLine(msg);
        }
    }
}
