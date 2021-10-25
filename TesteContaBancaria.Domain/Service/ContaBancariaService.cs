using System;
using TesteContaBancaria.Domain.ApiModel;
using TesteContaBancaria.Domain.Interface;

namespace TesteContaBancaria.Domain.Service
{
    public class ContaBancariaService : IContaBancariaService
    {
        public Result<ContaBancariaApiModel> Depositar(string email, string senha, double valor)
        {
            throw new NotImplementedException();
        }

        public Result<ContaBancariaApiModel> Obter(string email, string senha)
        {
            throw new NotImplementedException();
        }

        public Result<ContaBancariaApiModel> Sacar(string email, string senha, double valor)
        {
            throw new NotImplementedException();
        }
    }
}
