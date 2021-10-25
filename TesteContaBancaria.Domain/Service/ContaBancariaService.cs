using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TesteContaBancaria.Domain.ApiModel;
using TesteContaBancaria.Domain.Entidade;
using TesteContaBancaria.Domain.Interface;
using TesteContaBancaria.Domain.Util;

namespace TesteContaBancaria.Domain.Service
{
    public class ContaBancariaService : IContaBancariaService
    {
        public Result<ContaBancariaApiModel> Obter(string email, string senha)
        {
            List<Notification> mensagensErros = new List<Notification>();
            Validacao.ValidarEmailESenha(email, senha, mensagensErros);

            if (mensagensErros.Any())
                return Result<ContaBancariaApiModel>.Error(new ReadOnlyCollection<Notification>(mensagensErros));

            Cliente cliente = ClientesUtil.FiltrarLista(email, senha);

            return Result<ContaBancariaApiModel>.Ok(MontarResultado(cliente));

        }
        public Result<ContaBancariaApiModel> Depositar(string email, string senha, double valor)
        {
            List<Notification> mensagensErro = ValidarParametrosParaSacarEDepositar(email, senha, valor);

            if (mensagensErro.Any())
                return Result<ContaBancariaApiModel>.Error(new ReadOnlyCollection<Notification>(mensagensErro));

            Cliente cliente = ClientesUtil.FiltrarLista(email, senha);

            cliente.ContaBancaria.Depositar(valor);

            return Result<ContaBancariaApiModel>.Ok(new ContaBancariaApiModel()
            {
                IdConta = cliente.ContaBancaria.IdConta,
                Nome = cliente.Nome,
                Saldo = "R$ " + cliente.ContaBancaria.Saldo.ToString("N"),
                DataAbertura = cliente.ContaBancaria.DataAbertura.ToString("d")
            });
        }


        public Result<ContaBancariaApiModel> Sacar(string email, string senha, double valor)
        {

            List<Notification> mensagensErro = ValidarParametrosParaSacarEDepositar(email, senha, valor);

            if (mensagensErro.Any())
                return Result<ContaBancariaApiModel>.Error(new ReadOnlyCollection<Notification>(mensagensErro));


            Cliente cliente = ClientesUtil.FiltrarLista(email, senha);

            if (cliente.ContaBancaria.Saldo - valor < 0)
                return Result<ContaBancariaApiModel>.Error(new Notification(nameof(Sacar), Constantes.MENSAGEM_SALDO_INFUSICIENTE));


            cliente.ContaBancaria.Sacar(valor);
            
            return Result<ContaBancariaApiModel>.Ok(MontarResultado(cliente));
        }

        private List<Notification> ValidarParametrosParaSacarEDepositar(string email, string senha, double valor)
        {
            List<Notification> mensagensErros = new List<Notification>();
            Validacao.ValidarEmailESenha(email, senha, mensagensErros);
            if (valor < 0)
                mensagensErros.Add(new Notification(nameof(Sacar), Constantes.MENSAGEM_VALOR_POSITIVO));

            return mensagensErros;
        }


        private static ContaBancariaApiModel MontarResultado(Cliente cliente)
        {
            return new ContaBancariaApiModel()
            {
                IdConta = cliente.ContaBancaria.IdConta,
                Nome = cliente.Nome,
                Saldo = "R$ " + cliente.ContaBancaria.Saldo.ToString("N"),
                DataAbertura = cliente.ContaBancaria.DataAberturaConta(cliente.ContaBancaria.DataAbertura)
            };
        }
    }
}
