using Newtonsoft.Json;
using PizzaraCliente.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, ssl) => true;
        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        Console.Title = "Cliente Pizarra - Tester";
        var evento = new EventoDto
        {
            IdJuego = "JUEGO-003",
            Carrera = 2,
            Inning = 2,
            Abre = "Licey",
            Cierra = "Las Aguilas",
            Pelotero = "Leon Kennedy",
            FechaEvento = DateTime.Now
        };

        var baseUrl = "http://localhost:YOUR_PORT/"; // usa HTTP si es posible

        using (var client = new HttpClient { BaseAddress = new Uri(baseUrl) })
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var json = JsonConvert.SerializeObject(evento, Formatting.Indented);
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Enviando evento al servicio...");
            Console.ResetColor();
            Console.WriteLine(json);

            try
            {
                var resp = await client.PostAsync("api/pizarra/evento", content);
                var txt = await resp.Content.ReadAsStringAsync();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nRespuesta del servidor:");
                Console.ResetColor();
                Console.WriteLine(txt);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nERROR en la petición: " + ex.Message);
                Console.ResetColor();
            }
        }

        Console.WriteLine("\nPresiona una tecla para salir...");
        Console.ReadKey();
    }
}
