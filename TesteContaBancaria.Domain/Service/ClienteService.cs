using System;
using System.Collections.Generic;
using System.Text;
using TesteContaBancaria.ApiModel;
using TesteContaBancaria.Domain.Interface;

namespace TesteContaBancaria.Domain.Service
{
    public class ClienteService : IClienteService
    {
        public Result<ClienteApiModel> CadastrarCliente(ClienteApiModel clientes)
        {
            throw new NotImplementedException();


        }
        public Result<ClienteApiModel> ObterCliente(string email, string senha)
        {
            throw new NotImplementedException();
        }
        public Result ExcluirCliente(string email)
        {
            throw new NotImplementedException();
        }

        
    }
}
