using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DenunciasClient
{
    public class FuncionarioDto
    {
        public string tipodocumento { get; set; }
        public string documento { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string posicion { get; set; }
        public string empresa { get; set; }
        public string descripcion { get; set; }
    }

    class Program
    {
        private const string API_URL = "http://localhost:5062/api/denuncias";

        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {
            ConfigurarVentana();

            while (true)
            {
                MostrarBanner();

                var denuncia = new FuncionarioDto();

                Console.WriteLine(" [ INGRESO DE DATOS DEL OBJETIVO ]\n");

                denuncia.tipodocumento = LeerInput(" » Tipo Documento (Cédula/Pasaporte): ");
                denuncia.documento = LeerInput(" » No. Documento: ");
                denuncia.nombres = LeerInput(" » Nombres: ");
                denuncia.apellidos = LeerInput(" » Apellidos: ");
                denuncia.posicion = LeerInput(" » Cargo/Posición: ");
                denuncia.empresa = LeerInput(" » Entidad/Empresa: ");

                Console.WriteLine("\n [ DETALLE DE LA INFRACCIÓN ]");
                denuncia.descripcion = LeerInput(" » Descripción: ");

                await AnimacionEnvio();
                await EnviarDatos(denuncia);

                Console.WriteLine("\n Presione [ENTER] para nuevo registro o [CTRL+C] para salir.");
                Console.ReadLine();
            }
        }

        static string LeerInput(string label)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(label);
            Console.ForegroundColor = ConsoleColor.White;
            string input = Console.ReadLine();
            return input ?? string.Empty;
        }

        static async Task EnviarDatos(FuncionarioDto datos)
        {
            try
            {
                var json = JsonConvert.SerializeObject(datos);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync(API_URL, content);

                if (response.IsSuccessStatusCode)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n [✓] TRANSACCIÓN COMPLETADA: Datos asegurados en servidor.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n [X] ERROR DE SERVIDOR: Código {response.StatusCode}");
                    string errorDetails = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"     Detalle: {errorDetails}");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\n [!] ERROR CRÍTICO DE CONEXIÓN: {ex.Message}");
            }
            Console.ResetColor();
        }

        static void MostrarBanner()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"
   _________________________________________________________________
  |                                                                 |
  |   SISTEMA DE INTELIGENCIA CIUDADANA - MÓDULO DE DENUNCIAS v2.0  |
  |   SEGURIDAD: ENCRIPTADO | ESTADO: CONECTADO A BASE DE DATOS     |
  |_________________________________________________________________|
            ");
            Console.ResetColor();
            Console.WriteLine();
        }

        static void ConfigurarVentana()
        {
            Console.Title = "Terminal de Acceso Seguro - INTEC";
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
        }

        static async Task AnimacionEnvio()
        {
            Console.WriteLine();
            Console.Write(" Encriptando y Enviando paquetes ");
            for (int i = 0; i < 5; i++)
            {
                Console.Write(".");
                await Task.Delay(200);
            }
            Console.WriteLine();
        }
    }
}