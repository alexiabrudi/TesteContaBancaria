using System.Collections.Generic;
using System.Linq;
using TesteContaBancaria.Domain.Entidade;

namespace TesteContaBancaria.Domain.Util
{
    public static class ClientesUtil
    {
        public static List<Cliente> ListaCliente = new List<Cliente>()
        {
            new Cliente("Alexandre","alexiabrudi@gmail.com","mafukan7",new ContaBancaria(1)),
            new Cliente("Alexandre Carneiro","aleiabrudi@gmail.com","abc123",new ContaBancaria(2)),
        };

        public static Cliente FiltrarLista(string email, string senha)
        {
            return ListaCliente.Where(cliente => cliente.Email == email && cliente.Senha == senha).FirstOrDefault();
        }
    }
}
