using ServidorRegistroCliente.DataAccess;
using ServidorRegistroCliente.Models;

namespace ServidorRegistroCliente.Services
{
    public class DBServices
    {
        private readonly ClienteRepository _repo = new ClienteRepository();

        public string RegistrarCliente(Cliente cliente)
        {
            return _repo.InsertarCliente(cliente);
        }
    }
}
