using log4net;
using log4net.Config;
using ServidorRegistroCliente.Services;
using System;
using System.IO;
using System.Reflection;

namespace ServidorRegistroCliente
{
    class Program
    {
        static void Main(string[] args)
        {

            var repo = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(repo, new FileInfo("log4net.config"));

            string logFolder = @"C:\VS_Projects\DS3\Laboratorio\RegistroCliente\Logs";
            if (!Directory.Exists(logFolder))
                Directory.CreateDirectory(logFolder);

            Console.Title = "ServidorRegistroCliente";

            var server = new TcpServer();
            server.Start();
        }
    }
}
