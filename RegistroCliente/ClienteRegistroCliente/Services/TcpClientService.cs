using System;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using ClienteRegistroCliente.Models;

namespace ClienteRegistroCliente.Services
{
    public class TcpClientService
    {
        private const int PORT = 9000;
        private readonly string _serverIP;

        public TcpClientService(string serverIP)
        {
            _serverIP = serverIP;
        }

        public string EnviarCliente(Cliente cliente)
        {
            try
            {
                using (TcpClient client = new TcpClient())
                {
                    client.Connect(_serverIP, PORT);

                    var json = JsonConvert.SerializeObject(cliente);
                    byte[] data = Encoding.UTF8.GetBytes(json + "\n");

                    using (var stream = client.GetStream())
                    {
                        stream.Write(data, 0, data.Length);

                        byte[] buffer = new byte[256];
                        int bytesRead = stream.Read(buffer, 0, buffer.Length);

                        return Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    }
                }
            }
            catch (Exception ex)
            {
                return "ERROR_CLIENTE: " + ex.Message;
            }
        }
    }
}
