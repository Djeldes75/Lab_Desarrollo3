using log4net;
using Newtonsoft.Json;
using ServidorRegistroCliente.Models;
using ServidorRegistroCliente.Services;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServidorRegistroCliente.Services
{
    public class TcpServer
    {
        private readonly DBServices _dbServices = new DBServices();
        private TcpListener _listener;
        private const int PORT = 9000;
        private static readonly ILog log = LogManager.GetLogger(typeof(TcpServer));

        public void Start()
        {

            try
            {
                _listener = new TcpListener(IPAddress.Any, PORT);
                _listener.Start();

                PrintHeader();

                while (true)
                {
                    var client = _listener.AcceptTcpClient();
                    LogInfo("Cliente conectado.");
                    HandleClient(client);
                }
            }
            catch (Exception ex)
            {
                LogError("ERROR INICIANDO SERVIDOR: " + ex.Message);
            }
        }

        private void HandleClient(TcpClient client)
        {
            try
            {
                using (var stream = client.GetStream())
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    LogInfo("Mensaje recibido.");

                    var cliente = JsonConvert.DeserializeObject<Cliente>(message);

                    // Mostrar JSON formateado
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("\n=== JSON FORMATEADO ===");
                    Console.ResetColor();
                    Console.WriteLine(JsonConvert.SerializeObject(cliente, Formatting.Indented));

                    // Guardar en BD
                    var resultado = _dbServices.RegistrarCliente(cliente);

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"\n[OK] Resultado BD: {resultado}");
                    Console.ResetColor();

                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                LogError("ERROR EN HandleClient: " + ex.Message);
            }
            finally
            {
                client.Close();
                LogInfo("Cliente desconectado.\n");
            }
        }

        private void PrintHeader()
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("==============================================");
            Console.WriteLine("   SERVIDOR REGISTRO DE CLIENTES - ONLINE");
            Console.WriteLine("==============================================");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Escuchando en puerto {PORT}...\n");
            Console.ResetColor();
        }

        private void LogInfo(string msg)
        {
            log.Info(msg);

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"[INFO {DateTime.Now:HH:mm:ss}] ");
            Console.ResetColor();

            Console.WriteLine(msg);
        }

        private void LogError(string msg)
        {
            log.Error(msg);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"[ERROR {DateTime.Now:HH:mm:ss}] ");
            Console.ResetColor();

            Console.WriteLine(msg);
        }
    }
}
